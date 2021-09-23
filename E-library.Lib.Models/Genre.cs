using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_library.Lib.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string GenreType { get; set; }
        public List<Book> Books { get; set; }
    }
}
