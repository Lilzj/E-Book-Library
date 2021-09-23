using AutoMapper;
using E_library.Lib.DTO.Request;
using E_library.Lib.DTO.Response;
using E_library.Lib.Models;
using E_Library.Lib.Core.Interface;
using E_Library.Lib.Utilities.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace E_Library.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepo;
        private readonly IMapper _mapper;

        public RatingController(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepo = ratingRepository;
            _mapper = mapper;
        }

        [HttpGet("get-rating/{bookId}")]
        public IActionResult GetRating(int bookId)
        {
            if (User == null)
            {
                return Unauthorized("Only a logged in user can get a personal rating");
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var existingRating = _ratingRepo.GetRatingByUserAndBookId(userId, bookId);

            if (existingRating == null)
            {
                return NotFound("Rating not found");
            }

            var ratingResponse = _mapper.Map<Rating, RatingResponseDTO>(existingRating);

            return Ok(ratingResponse);
        }

        [HttpGet("get-average-rating/{bookId}")]
        public async Task<IActionResult> AverageRating(int bookId)
        {
            List<Rating> ratings = await _ratingRepo.GetListofRatingsByBookId(bookId);

            if (ratings.Count == 0)
            {
                return NotFound("No rating exists for this book yet");
            }

            double averageRate = ratings.CalculateTotalRating();

            return Ok(averageRate);
        }

        [HttpPost("add-rating")]
        public async Task<IActionResult> AddRating([FromBody] AddRatingDTO ratingDTO)
        {
            if (User == null)
            {
                return Unauthorized("Only a logged in user can add a book rating");
            }


            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var existingRating = _ratingRepo.GetRatingByUserAndBookId(userId, ratingDTO.BookId);

            if (existingRating != null)
            {
                return BadRequest("User has already made rating. (consider updating existing rating)");
            }

            var rating = _mapper.Map<AddRatingDTO, Rating>(ratingDTO);

            rating.UserId = userId;

            var isSuccess = await _ratingRepo.AddRatingAsync(rating);

            if (!isSuccess)
            {
                return StatusCode(500, "Rating not added");
            }

            var addRating = _ratingRepo.GetRatingByUserAndBookId(userId, ratingDTO.BookId);

            var ratingResponse = _mapper.Map<Rating, RatingResponseDTO>(addRating);

            return Ok(ratingResponse);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRating([FromBody] UpdateRatingDTO updateRatingDto)
        {
            if (User == null)
            {
                return Unauthorized("Only a logged in user can update a personal rating");
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var existingRating = _ratingRepo.GetRatingByUserAndBookId(userId, updateRatingDto.BookId);

            if (existingRating == null)
            {
                return BadRequest("Rating does not exist. Consider adding a rating");
            }

            existingRating.RatingNumber = updateRatingDto.RatingNumber;

            bool isSuccess = await _ratingRepo.UpdateRatingAsync(existingRating);

            if (!isSuccess)
            {
                return BadRequest("Rating not updated");
            }

            var updatedRating = _ratingRepo.GetRatingByUserAndBookId(userId, updateRatingDto.BookId);
            var response = _mapper.Map<Rating, RatingResponseDTO>(updatedRating);

            return Ok(response);

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            if (User == null)
            {
                return Unauthorized("Only a logged in user can delete a personal book rating");
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var ratingToDelete = _ratingRepo.GetRatingByUserAndBookId(userId, id);

            if (ratingToDelete == null)
            {
                return NotFound();
            }

            bool isSuccess = await _ratingRepo.DeleteRatingAsync(ratingToDelete);

            if (!isSuccess)
            {
                return StatusCode(500, "Sorry, Rating not deleted. Try again");
            }

            return Ok("Rating deleted");
        }
    }
}
