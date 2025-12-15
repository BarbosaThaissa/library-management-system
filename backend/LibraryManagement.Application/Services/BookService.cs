using LibraryManagement.Application.DTOs.Book;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services;

public class BookService(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IGenreRepository genreRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IGenreRepository _genreRepository = genreRepository;

    public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllWithRelationsAsync();

        return books.Select(b =>
            new BookResponseDto(
                b.Id,
                b.Title,
                b.AuthorId,
                b.Author!.Name,
                b.GenreId,
                b.Genre!.Name
            ));
    }

    public async Task<BookResponseDto?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdWithRelationsAsync(id);
        if (book == null) return null;

        return new BookResponseDto(
            book.Id,
            book.Title,
            book.AuthorId,
            book.Author!.Name,
            book.GenreId,
            book.Genre!.Name
        );
    }

    public async Task<BookResponseDto> CreateAsync(BookCreateDto dto)
    {
        if (!await _authorRepository.ExistsAsync(dto.AuthorId))
            throw new ArgumentException("Author not found");

        if (!await _genreRepository.ExistsAsync(dto.GenreId))
            throw new ArgumentException("Genre not found");

        var book = new Book
        {
            Title = dto.Title,
            AuthorId = dto.AuthorId,
            GenreId = dto.GenreId
        };

        var created = await _bookRepository.AddAsync(book);
        var fullBook = await _bookRepository.GetByIdWithRelationsAsync(created.Id);

        return new BookResponseDto(
            fullBook!.Id,
            fullBook.Title,
            fullBook.AuthorId,
            fullBook.Author!.Name,
            fullBook.GenreId,
            fullBook.Genre!.Name
        );
    }

    public async Task<BookResponseDto?> UpdateAsync(int id, BookUpdateDto dto)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) return null;

        if (!await _authorRepository.ExistsAsync(dto.AuthorId))
            throw new ArgumentException("Author not found");

        if (!await _genreRepository.ExistsAsync(dto.GenreId))
            throw new ArgumentException("Genre not found");

        book.Title = dto.Title;
        book.AuthorId = dto.AuthorId;
        book.GenreId = dto.GenreId;

        await _bookRepository.UpdateAsync(book);

        var updated = await _bookRepository.GetByIdWithRelationsAsync(id);

        return new BookResponseDto(
            updated!.Id,
            updated.Title,
            updated.AuthorId,
            updated.Author!.Name,
            updated.GenreId,
            updated.Genre!.Name
        );
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _bookRepository.DeleteAsync(id);
    }
}
