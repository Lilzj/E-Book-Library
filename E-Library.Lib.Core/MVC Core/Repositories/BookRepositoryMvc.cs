using E_library.Lib.DTO;
using E_library.Lib.DTO.MVCViewModels;
using E_library.Lib.DTO.Response;
using E_library.Lib.Models;
using E_Library.Lib.Core.MVC_Core.Interfaces;
using E_Library.Lib.Utilities.Helper.Pagination;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.MVC_Core.Repositories
{
    public class BookRepositoryMvc : IBookRepositoryMvc
    {
        HttpClient client = new HttpClient();
        public BookRepositoryMvc()
        {
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        }
 
        public Book GetBook(int id)
        {

            var book = new Book();
            using (client )
            {
                var task = client.GetAsync("http://localhost:54098/api/books/single/" + id);
                task.Wait();

                var result = task.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    book = JsonConvert.DeserializeObject<Book>(content);
                }
            }
            return book;
        }


        public async Task<PaginationFilter<Book>> GetBooks(string routeValue)
        {
            PaginationFilter<Book> output = default;
            using (client)
            {
                var getTask = client.GetAsync("http://localhost:54098/api/books/all-books/"+routeValue);

                getTask.Wait();

                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    output = JsonConvert.DeserializeObject<PaginationFilter<Book>>(content);
                }
            }
            return output;
        }

        public bool UpdeteBook(UpdateBookVM model)
        {
            var book = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(book, Encoding.UTF8, "application/json");

            using (client)
            {
                var putTask = client.PutAsync("http://localhost:54098/api/books/update-book/" + model.Id,requestContent);

                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public bool DeletBook(int id)
        {
            using (client)
            {
                var putTask = client.DeleteAsync("http://localhost:54098/api/books/delete-book/" + id);

                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddBook(AddBookDTO model)
        {
            using (client)
            {   
                if(model.BookImage.Length>0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        //Get the file stream from the multiform
                        model.BookImage.CopyToAsync(memoryStream).GetAwaiter().GetResult();

                        var form = new MultipartFormDataContent();
                        var fileContent = new ByteArrayContent(memoryStream.ToArray());
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        form.Add(fileContent, nameof(model.BookImage), model.BookImage.FileName);
                        form.Add(new StringContent(model.Author),nameof(model.Author));
                        form.Add(new StringContent(model.Description), nameof(model.Description));
                        form.Add(new StringContent(model.GenreId.ToString()), nameof(model.GenreId));
                        form.Add(new StringContent(model.ISBN), nameof(model.ISBN));
                        form.Add(new StringContent(model.PublishDate.ToString()), nameof(model.PublishDate));
                        form.Add(new StringContent(model.Publisher), nameof(model.Publisher));
                        form.Add(new StringContent(model.Title), nameof(model.Title));
                        form.Add(new StringContent(model.TotalPages.ToString()), nameof(model.TotalPages));
                     

                        var postTak = client.PostAsync("http://localhost:54098/api/books/add-book/", form).GetAwaiter().GetResult();

                        var result = postTak.Content.ReadAsStringAsync();

                        if (postTak.IsSuccessStatusCode)
                            return true;
                    }
                }

            }
            return false;
        }

        public async Task<SingleBookResponseDTO> GetBookById(int id)
        {

            SingleBookResponseDTO singleBook = default;
            using (client)
            {
                var getTask = client.GetAsync("http://localhost:54098/api/books/single/" + id);

                getTask.Wait();

                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    singleBook = JsonConvert.DeserializeObject<SingleBookResponseDTO>(content);
                }
            }
            return singleBook;
        }
    }
}
