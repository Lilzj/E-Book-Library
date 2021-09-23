using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_library.Lib.DTO.Request
{
    public class UpdateRatingDTO
    {
        public int BookId { get; set; }
        public int RatingNumber { get; set; }
    }
}
