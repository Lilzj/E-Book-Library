using E_library.Lib.Data;
using E_library.Lib.Models;
using E_Library.Lib.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DatabaseContext _context;

        public RatingRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Rating GetRatingByUserAndBookId(string userId, int bookId)
        {
            return _context.Ratings.FirstOrDefault(rating => rating.UserId == userId && rating.BookId == bookId);
        }

        public async Task<bool> AddRatingAsync(Rating rating)
        {
            bool isSuccess = false;

            _context.Ratings.Add(rating);

            if (await _context.SaveChangesAsync() > 0)
            {
                isSuccess = true;
            }

            return isSuccess;

        }

        public async Task<bool> UpdateRatingAsync(Rating rating)
        {
            bool isSuccess = false;

            _context.Ratings.Update(rating);

            if (await _context.SaveChangesAsync() > 0)
            {
                isSuccess = true;
            }
            return isSuccess;
        }


        public async Task<bool> DeleteRatingAsync(Rating rating)
        {
            bool isSuccesss = false;

            _context.Ratings.Remove(rating);

            if (await _context.SaveChangesAsync() > 0)
            {
                isSuccesss = true;
            }

            return isSuccesss;
        }

        public Task<List<Rating>> GetListofRatingsByBookId(int bookId)
        {
            var ratings = _context.Ratings.Select(rating => rating)
                            .Where(rating => rating.BookId == bookId)
                            .ToListAsync();

            return ratings;
        }
    }
}
