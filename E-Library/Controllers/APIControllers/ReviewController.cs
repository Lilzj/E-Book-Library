using AutoMapper;
using E_library.Lib.DTO.Request;
using E_library.Lib.DTO.Response;
using E_library.Lib.Models;
using E_Library.Lib.Core.Interface;
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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        [HttpGet("get-review/{bookId}")]
        public IActionResult GetReviewWithBookId(int bookId)
        {
            if (User == null)
            {
                return Unauthorized("Only a logged in user can get a personal review");
            }


            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var existingReview = _reviewRepo.GetReviewByUserAndBookId(userId, bookId);

            if (existingReview == null)
            {
                return NotFound("Review not found");
            }

            var reviewResponse = _mapper.Map<Review, ReviewResponseDTO>(existingReview);

            return Ok(reviewResponse);
        }

        [HttpGet("get-all-reviews-by-bookid/{bookId}")]
        public async Task<List<ReviewResponseDTO>> GetListOfReviewsByBookId(int bookId)
        {
            List<Review> reviews = await _reviewRepo.GetListOfReviewsWithBookId(bookId);

            var reviewsResponse = _mapper.Map<List<Review>, List<ReviewResponseDTO>>(reviews);

            return reviewsResponse;
        }



        [HttpPost("add-review")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewDTO addReviewDto)
        {
            if (User == null)
            {
                return Unauthorized("Only a logged in user can add a personal review");
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var existingReview = _reviewRepo.GetReviewByUserAndBookId(userId, addReviewDto.BookId);

            if (existingReview != null)
            {
                return BadRequest("User has already made a review for this book. (Consider updating existing review)");
            }


            var review = _mapper.Map<AddReviewDTO, Review>(addReviewDto);
            review.UserId = userId;


            var isSuccess = await _reviewRepo.AddReviewAsync(review);

            if (!isSuccess)
            {
                return StatusCode(500, "Review not added");
            }

            var newReview = _reviewRepo.GetReviewByUserAndBookId(userId, addReviewDto.BookId);
            var reviewResponse = _mapper.Map<Review, ReviewResponseDTO>(newReview);
            return Ok(reviewResponse);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDTO updateReviewDto)
        {
            if (User == null)
            {
                return Unauthorized("Only a logged in user can update an existing personal review");
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var existingReview = _reviewRepo.GetReviewByUserAndBookId(userId, updateReviewDto.BookId);


            if (existingReview == null)
            {
                return NotFound("You do have not made any review on this book yet. Consider adding a review");
            }


            existingReview.Description = updateReviewDto.Description;


            bool isSuccess = await _reviewRepo.UpdateReviewAsync(existingReview);

            if (!isSuccess)
            {
                return BadRequest("Review not updated");
            }
            var updateReview = _reviewRepo.GetReviewByUserAndBookId(userId, updateReviewDto.BookId);
            var updatedReviewResponse = _mapper.Map<Review, ReviewResponseDTO>(updateReview);

            return Ok(updatedReviewResponse);
        }

        [HttpDelete("delete/{bookId}")]
        public async Task<IActionResult> DeleteReview(int bookId)
        {
            if (User == null)
            {
                return Unauthorized("Only a logged in user can delete an existing personal review");
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var reviewToDelete = _reviewRepo.GetReviewByUserAndBookId(userId, bookId);

            if (reviewToDelete == null)
            {
                return NotFound();
            }


            bool isSuccess = await _reviewRepo.DeleteReviewAsync(reviewToDelete);

            if (!isSuccess)
            {
                return StatusCode(500, "Review not deleted");
            }

            return Ok("Review Deleted");
        }

    }
}
