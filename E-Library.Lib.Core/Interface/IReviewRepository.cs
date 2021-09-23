using E_library.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Interface
{
    public interface IReviewRepository
    {
        Task<bool> AddReviewAsync(Review review);

        Task<bool> UpdateReviewAsync(Review review);

        Review GetReviewByUserAndBookId(string userId, int bookId);

        Task<bool> DeleteReviewAsync(Review review);

        Task<List<Review>> GetListOfReviewsWithBookId(int bookId);
    }
}
