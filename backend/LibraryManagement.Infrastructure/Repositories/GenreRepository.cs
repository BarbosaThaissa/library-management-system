using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class GenreRepository(AppDbContext context) : IGenreRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Genre>> GetAllAsync() =>
        await _context.Genres.AsNoTracking().ToListAsync();

    public async Task<Genre?> GetByIdAsync(int id) =>
        await _context.Genres.FindAsync(id);

    public async Task<Genre> AddAsync(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        return genre;
    }

    public async Task UpdateAsync(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return false;

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id) =>
        await _context.Genres.AnyAsync(g => g.Id == id);
}
