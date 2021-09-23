using E_library.Lib.Data;
using E_library.Lib.DTO.Response;
using E_library.Lib.Models;
using E_Library.Lib.Core.Interface;
using E_Library.Lib.Utilities.Helper.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _ctx;

        public BookRepository(DatabaseContext DbContext)
        {
            _ctx = DbContext;
        }

        
        public async Task<BookResponse> AddBook(Book book)
        {

            await _ctx.Books.AddAsync(book);
            _ctx.SaveChanges();

            return new BookResponse(book);

        }


        public async Task<BookResponse> UpdateAsync(int id, Book book)
        {
            var ExistingBook = await _ctx.Books.FindAsync(id);


            if (ExistingBook == null)
                return new BookResponse("Book not found");

            ExistingBook.Title = book.Title;
            ExistingBook.Author = book.Author;
            //ExistingBook.GenreId = book.GenreId;
            ExistingBook.Description = book.Description;
            ExistingBook.ISBN = book.ISBN;
            ExistingBook.PublishDate = book.PublishDate;
            ExistingBook.Publisher = book.Publisher;
            ExistingBook.TotalPages = book.TotalPages;
            //ExistingBook.ImageUrl = book.ImageUrl;


            _ctx.Books.Update(ExistingBook);
            _ctx.SaveChanges();

            return new BookResponse(ExistingBook);

        }

        public async Task<BookResponse> DeleteAsync(int id)
        {
            var ExistingBook = await _ctx.Books.FindAsync(id);

            if (ExistingBook == null)
                return new BookResponse("Book not found");

            _ctx.Books.Remove(ExistingBook);
            _ctx.SaveChanges();

            return new BookResponse(ExistingBook);

        }

        public async Task<PaginationFilter<Book>> GetAllBooksAsync(int pageNumber, int pageSize)
        {
            var data = await _ctx.Books.ToListAsync();
             return  PaginationFilter<Book>.PaginationSet(data,pageNumber, pageSize);
        }

        public async Task<IEnumerable<Book>> GetBooksBySearch(string SearchQuery)
        {

            var books = await _ctx.Books
                .Where(x => x.Author.ToLower().Contains(SearchQuery.ToLower()) ||
                x.Title.ToLower().Contains(SearchQuery.ToLower()) || 
                x.Publisher.ToLower().Contains(SearchQuery.ToLower())||
                x.ISBN.ToLower().Contains(SearchQuery.ToLower())).ToListAsync();

            return books;                         
        }

        public async Task<SingleBookResponseDTO> GetBookById(int id)
        {
            var book = await _ctx.Books.Where(x => x.Id == id).SingleOrDefaultAsync();
            var GenreName = await _ctx.Genres.Where(x => x.Id == book.GenreId).Select(x => x.title).SingleOrDefaultAsync();

            var SingleItem = new SingleBookResponseDTO
            {
                
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                CopiesInLibrary = book.CopiesInLibrary,
                CopiesTakenOut = book.CopiesTakenOut,
                PublishDate = book.PublishDate,
                ImageUrl = book.ImageUrl,
                Reviews = book.Reviews,
                ISBN = book.ISBN,
                TotalPages = book.TotalPages,
                Ratings = book.Ratings,
                GenreName = GenreName,
                Publisher = book.Publisher,
                Author = book.Author
            };

            return SingleItem;
        }
    }
}
