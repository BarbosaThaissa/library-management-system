using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAllAsync();
    Task<Genre?> GetByIdAsync(int id);
    Task<Genre> AddAsync(Genre genre);
    Task UpdateAsync(Genre genre);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
