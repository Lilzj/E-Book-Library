using E_Library.Lib.Core.MVC_Core.Interfaces;
using E_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace E_Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepositoryMvc _bookRepo;
        private readonly IAuthServices _authservices;

        public HomeController(ILogger<HomeController> logger, IBookRepositoryMvc bookRepository, IAuthServices authServices)
        {
            _logger = logger;
            _bookRepo = bookRepository;
            _authservices = authServices;
        }

        public async Task<IActionResult> Index(int pagenumber)
        {
            var books = await _bookRepo.GetBooks(pagenumber.ToString());
            _logger.LogInformation(HttpContext.User.Identity.Name);

            return View(books);
        
        }
   
 
        public IActionResult SingleBook()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
