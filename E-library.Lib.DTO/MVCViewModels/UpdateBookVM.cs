using Microsoft.AspNetCore.Http;
using System;

namespace E_library.Lib.DTO.MVCViewModels
{
    public class UpdateBookVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public int GenreId { get; set; }
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public int TotalPages { get; set; }
        public IFormFile BookImage { get; set; }
        public string Author { get; set; }
    }
}
