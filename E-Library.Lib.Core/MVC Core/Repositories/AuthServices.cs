using E_library.Lib.DTO;
using E_Library.Lib.Core.MVC_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.MVC_Core.Repositories
{
    public class AuthServices : IAuthServices
    {
        public AuthReturnDto Login(LoginDto loginDto)
        {
            using (var client = new HttpClient())
            {
                var postTask = client.PostAsJsonAsync<LoginDto>("http://localhost:54098/api/auth/login", loginDto);
                postTask.Wait();

                var check = postTask.Result.Content;

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var returned = result.Content.ReadAsStringAsync();
                    var userToReturn = new AuthReturnDto
                    {
                        UserId = returned.Result.Split("\":\"")[1].Split("\",\"")[0],
                        FirstName = returned.Result.Split("\":\"")[2].Split("\",\"")[0],
                        LastName = returned.Result.Split("\":\"")[3].Split("\",\"")[0],
                        Token = returned.Result.Split("\":\"")[4].Split("\",\"")[0].Split("= ")[1].Split(",")[0]
                    };

                    return userToReturn;

                }

                return null;
            }
        }

        public AuthReturnDto RegisterUser(RegisterDto registeruser)
        {
            using (var client = new HttpClient())
            {

                var postTask = client.PostAsJsonAsync<RegisterDto>("http://localhost:54098/api/auth/register", registeruser);
                postTask.Wait();
                var result = postTask.Result;
                if (!result.IsSuccessStatusCode)
                    return null;

                var user = new LoginDto
                {
                    Email = registeruser.Email,
                    Password = registeruser.Password,
                    RememberMe = false,
                };
                var login = Login(user);
                if (login == null)
                    return null;

                return login;
            }
        }
    }
}
