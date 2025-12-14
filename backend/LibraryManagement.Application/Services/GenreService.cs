using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;

    public GenreService(IGenreRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Genre>> GetAllAsync() =>
        _repository.GetAllAsync();

    public Task<Genre?> GetByIdAsync(int id) =>
        _repository.GetByIdAsync(id);

    public Task<Genre> CreateAsync(Genre genre) =>
        _repository.AddAsync(genre);

    public async Task<Genre?> UpdateAsync(Genre genre)
    {
        var exists = await _repository.GetByIdAsync(genre.Id);
        if (exists == null)
            return null;

        return await _repository.UpdateAsync(genre);
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
