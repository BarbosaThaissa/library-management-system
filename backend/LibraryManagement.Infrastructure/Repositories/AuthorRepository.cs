using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class AuthorRepository(AppDbContext context) : IAuthorRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Author>> GetAllAsync() =>
        await _context.Authors.AsNoTracking().ToListAsync();

    public async Task<Author?> GetByIdAsync(int id) =>
        await _context.Authors.FindAsync(id);

    public async Task<Author> AddAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id) =>
        await _context.Authors.AnyAsync(a => a.Id == id);
}
