using AutoMapper;
using E_library.Lib.DTO;
using E_library.Lib.DTO.Request;
using E_library.Lib.Models;
using E_Library.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Library.Controllers.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //Injected Services
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;


        public AuthController(UserManager<User> userManager, 
                              SignInManager<User> signInManager, 
                              IConfiguration config, 
                              IMapper mapper, 
                              RoleManager<IdentityRole> roleManager,
                              IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            //Get user from Database
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return BadRequest("User Not Found");

            //signin user
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
                return Unauthorized();

            try
            {
                //Get user roles
                var roles = await _userManager.GetRolesAsync(user);
                var token = TokenHandler.GenerateToken(user, roles, _config);//Generate token
                var userToReturn = new AuthReturnDto
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = token.ToString()
                };
                
                return Ok(userToReturn);
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong generating token");
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            //Check if email exists
            if (await _userManager.FindByEmailAsync(model.Email) != null)
                return BadRequest("Email already exists");

            //Map dto to model
            var user = _mapper.Map<RegisterDto, User>(model);
            user.UserName = model.Email;

            //Check if role exists and create if not
            var role = await _roleManager.FindByNameAsync("Regular");
            if (role == null)
                await _roleManager.CreateAsync(new IdentityRole("Regular"));


            var result = await _userManager.CreateAsync(user, model.Password);

            //add user to role
            await _userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
                return BadRequest();

            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        [Route("all-users")]
        public IActionResult GetAllUsers()
        {
            var users =   _userManager.Users;
            if (User == null)
                return BadRequest("No user in the database");

            return Ok(users);
        }



        [HttpGet("{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("User with email not found");

            var response = _mapper.Map<User, UserResponse>(user);
            return Ok(response);
        }

        [HttpGet("username/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return BadRequest("User with username not found");

            var response = _mapper.Map<User, UserResponse>(user);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return BadRequest("This user does not exist");

            var res = await _userManager.DeleteAsync(user);

            if (!res.Succeeded)
                return BadRequest("Failed to remove user");

            return Ok($"Removed user with id: {user.Id}");
        }


        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto model)
        {
            var userFromDatabase = await _userManager.FindByIdAsync(model.Id);

            userFromDatabase.FirstName = model.FirstName;
            userFromDatabase.LastName = model.LastName;
            userFromDatabase.Email = model.Email;

            var result = await _userManager.UpdateAsync(userFromDatabase);
            if (!result.Succeeded)
                return BadRequest();

            return Ok(userFromDatabase);
        }



        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
            
        }
       
        [HttpGet("current-user")]
        public async Task<IActionResult> CurrentUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _userManager.FindByIdAsync(userId);

                return Ok(user);
            }
            return Unauthorized();
        }
    }
}
