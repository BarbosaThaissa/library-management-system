namespace LibraryManagement.Application.DTOs.Book;

public record BookCreateDto(
    string Title,
    int AuthorId,
    int GenreId
);
