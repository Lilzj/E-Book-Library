using AutoMapper;
using E_library.Lib.DTO;
using E_library.Lib.DTO.MVCViewModels;
using E_library.Lib.Models;
using E_Library.Lib.Core.MVC_Core.Interfaces;
using E_Library.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Library.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IBookRepositoryMvc _bookRepo;
        private readonly IMapper _mapper; 

        public DashboardController(IBookRepositoryMvc repo, IMapper mapper)
        {
            _bookRepo = repo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page)
        {
            var books = await _bookRepo.GetBooks(page.ToString());
            var response = new DashBoardViewModel
            {
                Books = books,
            };

            return View(response);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(AddBookDTO model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var response = _bookRepo.AddBook(model);
            if (!response)
                return View(model);

            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult UpdateBook(int id)
        {
            var book = _bookRepo.GetBook(id);
            var response = _mapper.Map<Book,UpdateBookVM>(book);
            var viewmodel = new UpdateBookViewModel
            {
                Book = response
            };
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult UpdateBook(UpdateBookViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = _bookRepo.UpdeteBook(model.Book);
            if (!result)
                return View(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteBook(int id)
        {
            var result = _bookRepo.DeletBook(id);
            if (!result)
                return View();
            return RedirectToAction("Index");
        }
        
        public IActionResult AddGenre()
        {
            return View();
        }

    }
}
