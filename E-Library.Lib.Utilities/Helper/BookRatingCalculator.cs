using E_library.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Utilities.Helper
{
    public static class BookRatingCalculator
    {
        public static double CalculateTotalRating(this List<Rating> ratings)
        {
            int sum = 0;

            foreach (var rating in ratings)
            {
                sum += rating.RatingNumber;
            }

            return (double)sum / ratings.Count;
        }
    }
}
