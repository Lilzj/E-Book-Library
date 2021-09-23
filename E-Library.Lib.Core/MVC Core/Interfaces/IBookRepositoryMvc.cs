using E_library.Lib.DTO;
using E_library.Lib.DTO.MVCViewModels;
using E_library.Lib.DTO.Response;
using E_library.Lib.Models;
using E_Library.Lib.Utilities.Helper.Pagination;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.MVC_Core.Interfaces
{
    public interface IBookRepositoryMvc
    {
        Task<PaginationFilter<Book>> GetBooks(string routeValue);
        Book GetBook(int id);
        bool UpdeteBook(UpdateBookVM model);
        bool DeletBook(int id);
        bool AddBook(AddBookDTO model);
        Task<SingleBookResponseDTO> GetBookById(int id);
    }
}
