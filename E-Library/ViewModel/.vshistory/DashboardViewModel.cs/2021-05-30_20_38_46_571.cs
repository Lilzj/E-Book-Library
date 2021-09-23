using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.ViewModels
{
    public class DashBoardViewModel
    {
        //Admin
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string AdminImageUrl { get; set; }
        //books
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
        //User
        public int UserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Email { get; set; }
        public string UserImageUrl { get; set; }

    }
}
