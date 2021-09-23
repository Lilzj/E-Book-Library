using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_library.Lib.DTO.Response
{
    public class RatingResponseDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int RatingNumber { get; set; }
    }
}
