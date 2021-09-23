using E_library.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Interface
{
    public interface IRatingRepository
    {
        Rating GetRatingByUserAndBookId(string userId, int bookId);

        Task<bool> AddRatingAsync(Rating rating);

        Task<bool> UpdateRatingAsync(Rating rating);

        Task<List<Rating>> GetListofRatingsByBookId(int bookId);
        Task<bool> DeleteRatingAsync(Rating rating);
    }
}
