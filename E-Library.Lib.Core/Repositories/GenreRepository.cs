using E_library.Lib.Data;
using E_library.Lib.Models;
using E_Library.Lib.Core.Interface;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Implementation
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DatabaseContext _context;

        public GenreRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> AddGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
