using E_library.Lib.Data;
using E_library.Lib.Models;
using E_Library.Lib.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DatabaseContext _context;

        public ReviewRepository(DatabaseContext dbContext)
        {
            _context = dbContext;
        }


        public Review GetReviewByUserAndBookId(string userId, int bookId)
        {
            Review review = _context.Review.FirstOrDefault(review => review.BookId == bookId && review.UserId == userId);
            return review;
        }

        public async Task<bool> AddReviewAsync(Review review)
        {
            bool isSuccess = false;

            await _context.Review.AddAsync(review);

            int check = await _context.SaveChangesAsync();

            if (check > 0)
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public async Task<bool> UpdateReviewAsync(Review review)
        {
            bool isSuccess = false;

            _context.Update(review);

            int check = await _context.SaveChangesAsync();
            if (check > 0)
            {
                isSuccess = true;
            }

            return isSuccess;

        }


        public async Task<bool> DeleteReviewAsync(Review review)
        {
            bool isSuccess = false;

            _context.Review.Remove(review);

            int check = await _context.SaveChangesAsync();

            if (check > 0)
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public async Task<List<Review>> GetListOfReviewsWithBookId(int bookId)
        {
            return await _context.Review.Where(review => review.BookId == bookId)
                                    .Select(review => review)
                                    .ToListAsync();
        }
    }
}
