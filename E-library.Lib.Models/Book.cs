using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_library.Lib.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CopiesInLibrary { get; set; }
        public int CopiesTakenOut { get; set; }
        public int GenreId { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public int TotalPages { get; set; }
        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();

    }
}
