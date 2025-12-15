using LibraryManagement.Application.DTOs.Genre;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services;

public class GenreService(IGenreRepository repository) : IGenreService
{
    private readonly IGenreRepository _repository = repository;

    public async Task<IEnumerable<GenreResponseDto>> GetAllAsync()
    {
        var genres = await _repository.GetAllAsync();

        return genres.Select(g =>
            new GenreResponseDto(g.Id, g.Name));
    }

    public async Task<GenreResponseDto?> GetByIdAsync(int id)
    {
        var genre = await _repository.GetByIdAsync(id);
        if (genre == null) return null;

        return new GenreResponseDto(genre.Id, genre.Name);
    }

    public async Task<GenreResponseDto> CreateAsync(GenreCreateDto dto)
    {
        var genre = new Genre
        {
            Name = dto.Name
        };

        var created = await _repository.AddAsync(genre);

        return new GenreResponseDto(created.Id, created.Name);
    }

    public async Task<GenreResponseDto?> UpdateAsync(int id, GenreUpdateDto dto)
    {
        var genre = await _repository.GetByIdAsync(id);
        if (genre == null) return null;

        genre.Name = dto.Name;

        await _repository.UpdateAsync(genre);

        return new GenreResponseDto(genre.Id, genre.Name);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
