using E_library.Lib.DTO;
using E_library.Lib.DTO.Response;
using E_library.Lib.Models;
using E_Library.Lib.Utilities.Helper.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Interface
{
    public interface IBookRepository
    {
        Task<BookResponse> AddBook(Book book);
        Task<BookResponse> UpdateAsync(int id, Book book);
        Task<BookResponse> DeleteAsync(int id);
        Task<PaginationFilter<Book>> GetAllBooksAsync(int pageNumber, int pagesize);
        Task<IEnumerable<Book>> GetBooksBySearch(string SearchQuery);
        Task<SingleBookResponseDTO> GetBookById(int id);
    }
}
