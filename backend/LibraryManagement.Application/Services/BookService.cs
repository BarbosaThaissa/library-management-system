using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IGenreRepository _genreRepository;

    public BookService(
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        IGenreRepository genreRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
    }

    public Task<IEnumerable<Book>> GetAllAsync() =>
        _bookRepository.GetAllAsync();

    public Task<Book?> GetByIdAsync(int id) =>
        _bookRepository.GetByIdAsync(id);

    public async Task<Book?> CreateAsync(Book book)
    {
        if (!await AuthorExists(book.AuthorId))
            return null;

        if (!await GenreExists(book.GenreId))
            return null;

        return await _bookRepository.AddAsync(book);
    }

    public async Task<Book?> UpdateAsync(Book book)
    {
        var exists = await _bookRepository.GetByIdAsync(book.Id);
        if (exists == null)
            return null;

        if (!await AuthorExists(book.AuthorId))
            return null;

        if (!await GenreExists(book.GenreId))
            return null;

        return await _bookRepository.UpdateAsync(book);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _bookRepository.GetByIdAsync(id);
        if (exists == null)
            return false;

        await _bookRepository.DeleteAsync(id);
        return true;
    }

    private async Task<bool> AuthorExists(int authorId) =>
        await _authorRepository.GetByIdAsync(authorId) != null;

    private async Task<bool> GenreExists(int genreId) =>
        await _genreRepository.GetByIdAsync(genreId) != null;
}
