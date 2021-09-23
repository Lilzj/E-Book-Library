using System;
using System.ComponentModel.DataAnnotations;

namespace E_library.Lib.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
