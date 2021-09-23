using AutoMapper;
using E_Library.Lib.Core.Interface;
using E_Library.Lib.Core.MVC_Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepositoryMvc _bookRepo;
        private readonly IMapper _mapper;

        public BooksController(IBookRepositoryMvc repo, IMapper mapper)
        {
            _bookRepo = repo;
            _mapper = mapper;
        }

        public IActionResult GetAllBooks()
        {
            var books = _bookRepo.GetBooks("");
            return View();
        }

        public async Task<IActionResult> GetSingleBook(int id)
        {
            var book = await _bookRepo.GetBookById(id);

            return View("SingleBook",book);
        }

    }
}
