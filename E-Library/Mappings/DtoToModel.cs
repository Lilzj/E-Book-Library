using AutoMapper;
using E_library.Lib.DTO;
using E_library.Lib.DTO.Request;
using E_library.Lib.Models;

namespace E_Library.Mappings
{
    public class DtoToModel:Profile
    {
        public DtoToModel()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<AddBookDTO, Book>();
            CreateMap<AddGenreDto, Genre>();
            CreateMap<AddReviewDTO, Review>();
            CreateMap<UpdateReviewDTO, Review>();
            CreateMap<AddRatingDTO, Rating>();
        }
    }
}
