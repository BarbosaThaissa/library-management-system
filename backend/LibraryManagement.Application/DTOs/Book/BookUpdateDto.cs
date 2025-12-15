namespace LibraryManagement.Application.DTOs.Book;

public record BookUpdateDto(
    string Title,
    int AuthorId,
    int GenreId
);
