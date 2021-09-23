using E_library.Lib.DTO;
using E_Library.Lib.Core.MVC_Core.Interfaces;
using E_Library.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthServices _authservices;
        private readonly ILogger _log;

        public AccountController(IAuthServices authServices, ILogger<AccountController> log)
        {
            _authservices = authServices;
            _log = log;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
                return View();
            var user = new LoginDto
            {
                Email = loginModel.Email,
                Password = loginModel.Password,
                RememberMe = loginModel.Rememberme
            };
            var loggedInUser = _authservices.Login(user);

            if (loggedInUser != null)
            {
                HttpContext.Session.SetString("JWToken",loggedInUser.Token);
                var token = HttpContext.Session.GetString("JWToken");
                _log.LogInformation(token);
                
                return RedirectToAction("Index", "Home");
               

            }

            ModelState.AddModelError(string.Empty, "server error");
            return View("Index");
        }

        [HttpPost]
        public IActionResult Register(SignUpViewModel registerModel)
        {
            if (!ModelState.IsValid)
                return View();
            var user = new RegisterDto
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                Password = registerModel.Password,
                ImageUrl = registerModel.ImageUrl
            };
            var registeredUser = _authservices.RegisterUser(user);

            if (registeredUser != null)
            {
                return RedirectToAction("Index", "Home", registeredUser);
            }

            ModelState.AddModelError(string.Empty, "server error");
            return View("Index");
        }
    }
}
