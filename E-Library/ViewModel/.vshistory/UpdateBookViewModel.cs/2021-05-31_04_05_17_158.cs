using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.ViewModel
{
    public class UpdateBookViewModel
    {
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookIsbn { get; set; }
        public string Authur { get; set; }
        public string Publisher { get; set; }
        public DateTime DatePublished { get; set; }
        public string Genre { get; set; }
        public string BookDescription { get; set; }
        public string BookUrl { get; set; }
        public int TotalCopies { get; set; }
        public int TotalCopiesTaken { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
