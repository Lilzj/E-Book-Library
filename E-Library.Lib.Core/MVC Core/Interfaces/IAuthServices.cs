using E_library.Lib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.MVC_Core.Interfaces
{
    public interface IAuthServices
    {
        AuthReturnDto RegisterUser(RegisterDto registeruser);
        AuthReturnDto Login(LoginDto loginDto);
    }
}
