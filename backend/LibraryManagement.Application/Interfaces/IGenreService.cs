using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<Genre>> GetAllAsync();
    Task<Genre?> GetByIdAsync(int id);
    Task<Genre> CreateAsync(Genre genre);
    Task<Genre?> UpdateAsync(Genre genre);
    Task<bool> DeleteAsync(int id);
}
