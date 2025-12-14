using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Author>> GetAllAsync() =>
        _repository.GetAllAsync();

    public Task<Author?> GetByIdAsync(int id) =>
        _repository.GetByIdAsync(id);

    public Task<Author> CreateAsync(Author author) =>
        _repository.AddAsync(author);

    public async Task<Author?> UpdateAsync(Author author)
    {
        var exists = await _repository.GetByIdAsync(author.Id);
        if (exists == null)
            return null;

        return await _repository.UpdateAsync(author);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.GetByIdAsync(id);
        if (exists == null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}
