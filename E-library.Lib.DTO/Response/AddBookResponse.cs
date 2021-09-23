using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_library.Lib.DTO.Response
{
    public class AddBookResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public int GenreId { get; set; }
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public int TotalPages { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
    }
}
