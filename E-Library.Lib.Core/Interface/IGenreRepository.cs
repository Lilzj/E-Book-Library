using E_library.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Lib.Core.Interface
{
    public interface IGenreRepository
    {
        Task<int> AddGenreAsync(Genre genre);
    }
}
