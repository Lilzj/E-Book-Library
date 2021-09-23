using E_library.Lib.Models;
using E_Library.Lib.Utilities.Helper.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.ViewModel
{
    public class DashBoardViewModel
    {
        public PaginationFilter<Book> Books { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
