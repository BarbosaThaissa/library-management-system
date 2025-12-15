using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<IEnumerable<Book>> GetAllWithRelationsAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book?> GetByIdWithRelationsAsync(int id);
    Task<Book> AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task<bool> DeleteAsync(int id);
}
