using LibraryManagement.Application.DTOs.Author;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services;

public class AuthorService(IAuthorRepository repository) : IAuthorService
{
    private readonly IAuthorRepository _repository = repository;

    public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
    {
        var authors = await _repository.GetAllAsync();

        return authors.Select(a =>
            new AuthorResponseDto(a.Id, a.Name));
    }

    public async Task<AuthorResponseDto?> GetByIdAsync(int id)
    {
        var author = await _repository.GetByIdAsync(id);
        if (author == null) return null;

        return new AuthorResponseDto(author.Id, author.Name);
    }

    public async Task<AuthorResponseDto> CreateAsync(AuthorCreateDto dto)
    {
        var author = new Author
        {
            Name = dto.Name
        };

        var created = await _repository.AddAsync(author);

        return new AuthorResponseDto(created.Id, created.Name);
    }

    public async Task<AuthorResponseDto?> UpdateAsync(int id, AuthorUpdateDto dto)
    {
        var author = await _repository.GetByIdAsync(id);
        if (author == null) return null;

        author.Name = dto.Name;

        await _repository.UpdateAsync(author);

        return new AuthorResponseDto(author.Id, author.Name);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
