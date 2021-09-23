using E_library.Lib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace E_Library.Utilities
{
    public class TokenHandler
    {
        public static Object GenerateToken(User loggedinUser, IList<string> roles, IConfiguration config)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loggedinUser.UserName),
                new Claim(ClaimTypes.Email, loggedinUser.Email),
                new Claim(ClaimTypes.NameIdentifier, loggedinUser.Id)
            };


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Create security token descriptor
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:JwtSigninKey"]));
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenCreated = tokenHandler.CreateToken(securityTokenDescriptor);

            return new { token = tokenHandler.WriteToken(tokenCreated), id = loggedinUser.Id };
        }
    }
}
