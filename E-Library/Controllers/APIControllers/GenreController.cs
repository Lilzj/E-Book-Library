using AutoMapper;
using E_library.Lib.DTO;
using E_library.Lib.Models;
using E_Library.Lib.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Library.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpPost("add-genre")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddGenre([FromBody] AddGenreDto model)
        {
            var genre = _mapper.Map<AddGenreDto, Genre>(model);
            var res = await _genreRepository.AddGenreAsync(genre);

            if (res <= 0)
                return BadRequest("Genre not created");

            return Ok(model);
        }

    }
}
