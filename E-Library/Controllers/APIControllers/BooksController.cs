using AutoMapper;
using E_library.Lib.DTO;
using E_library.Lib.DTO.Response;
using E_library.Lib.Models;
using E_Library.Lib.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Library.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _repo;
        private readonly IFileUploadService _fileUpload;
        private readonly IConfiguration _config;

        public BooksController(IMapper map, IBookRepository repo, IFileUploadService fileUpload, IConfiguration config)
        {
            _mapper = map;
            _repo = repo;
            _fileUpload = fileUpload;
            _config = config;
        }


        [HttpPost("add-book")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook([FromForm] AddBookDTO model)
        {

            var book = _mapper.Map<AddBookDTO, Book>(model);

            //upload image
            var imageUrl = _fileUpload.UploadImage(model.BookImage);

            if (string.IsNullOrWhiteSpace(imageUrl))
                return BadRequest("Image upload failed");

            book.ImageUrl = imageUrl;
            var res = await _repo.AddBook(book);

            if (!res.Success)
                return BadRequest(res.Message);

            //Attach image to book
            book.ImageUrl = imageUrl;

            var BookCreated = _mapper.Map<Book, AddBookResponse>(res.Book);
            return CreatedAtAction(nameof(AddBook), BookCreated);

        }


        [HttpPut("update-book/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] AddBookDTO model)
        {
            var book = _mapper.Map<AddBookDTO, Book>(model);
            var res = await _repo.UpdateAsync(id, book);

            if (!res.Success)
                return BadRequest(res.Message);

            var BookUpdated = _mapper.Map<Book, AddBookResponse>(res.Book);
            return Ok(BookUpdated);
        }


        [HttpDelete("delete-book/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _repo.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var BookDeleted = _mapper.Map<Book, AddBookDTO>(result.Book);

            return Ok(BookDeleted);
        }


        [HttpGet("all-books/{pagenumber}")]
        public async Task<IActionResult> GetAllBooks(int pagenumber)
        {
            try
            {
                var pageSize = Convert.ToInt32(_config["Pagination:PageSize"]);
                var books = await _repo.GetAllBooksAsync(pagenumber,pageSize);
                return Ok(books);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("search/{searchQuery}")]
        public async Task<IActionResult> GetBooksBySearch(string searchQuery)
        {
            if (String.IsNullOrWhiteSpace(searchQuery))
            {
                return BadRequest("Please enter a search term");
            }

            var books = await _repo.GetBooksBySearch(searchQuery);
            var response = _mapper.Map<IEnumerable<Book>, IEnumerable<AddBookDTO>>(books);

            return Ok(response);
        }


        [HttpGet("single/{id}")]
        //[Authorize]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _repo.GetBookById(id);
            return Ok(book);
        }
    }
}
