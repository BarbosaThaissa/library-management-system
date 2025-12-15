namespace LibraryManagement.Application.DTOs.Book;

public record BookResponseDto(
    int Id,
    string Title,
    int AuthorId,
    string AuthorName,
    int GenreId,
    string GenreName
);
