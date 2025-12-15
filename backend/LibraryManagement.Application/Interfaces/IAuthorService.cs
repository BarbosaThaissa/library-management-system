using LibraryManagement.Application.DTOs.Author;

namespace LibraryManagement.Application.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorResponseDto>> GetAllAsync();
    Task<AuthorResponseDto?> GetByIdAsync(int id);
    Task<AuthorResponseDto> CreateAsync(AuthorCreateDto dto);
    Task<AuthorResponseDto?> UpdateAsync(int id, AuthorUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
