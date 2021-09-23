using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace E_library.Lib.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
