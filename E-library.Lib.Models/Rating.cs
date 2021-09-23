using System.ComponentModel.DataAnnotations;

namespace E_library.Lib.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public int RatingNumber { get; set; }
    }
}
