using E_library.Lib.DTO;
using AutoMapper;
using E_library.Lib.Models;
using E_library.Lib.DTO.Response;
using E_library.Lib.DTO.MVCViewModels;

namespace E_Library.Mappings
{
    public class ModelToDto : Profile
    {
        public ModelToDto()
        {
            CreateMap<Book, AddBookDTO>();
            CreateMap<Book, UpdateBookVM>();
            CreateMap<Book, AddBookResponse>();
            CreateMap<Book, BookResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<Review, ReviewResponseDTO>();
            CreateMap<Rating, RatingResponseDTO>();
        }
       
    }
}
