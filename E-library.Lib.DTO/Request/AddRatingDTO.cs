using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_library.Lib.DTO.Request
{
    public class AddRatingDTO
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int RatingNumber { get; set; }
    }
}
