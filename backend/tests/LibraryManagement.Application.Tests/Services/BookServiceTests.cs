using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LibraryManagement.Application.DTOs.Book;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using Moq;
using Xunit;

namespace LibraryManagement.Application.Tests.Services;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<IGenreRepository> _genreRepositoryMock;
    private readonly BookService _service;

    public BookServiceTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _genreRepositoryMock = new Mock<IGenreRepository>();

        _service = new BookService(
            _bookRepositoryMock.Object,
            _authorRepositoryMock.Object,
            _genreRepositoryMock.Object
        );
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfBooks()
    {
        var books = new List<Book>
        {
            new()
            {
                Id = 1,
                Title = "Book 1",
                AuthorId = 1,
                Author = new Author { Id = 1, Name = "Author 1" },
                GenreId = 1,
                Genre = new Genre { Id = 1, Name = "Genre 1" }
            }
        };

        _bookRepositoryMock
            .Setup(r => r.GetAllWithRelationsAsync())
            .ReturnsAsync(books);

        var result = await _service.GetAllAsync();

        result.Should().HaveCount(1);
        result.First().AuthorName.Should().Be("Author 1");
        result.First().GenreName.Should().Be("Genre 1");
    }

    [Fact]
    public async Task GetByIdAsync_WhenBookExists_ShouldReturnBook()
    {
        var book = new Book
        {
            Id = 1,
            Title = "Book",
            AuthorId = 1,
            Author = new Author { Id = 1, Name = "Author" },
            GenreId = 1,
            Genre = new Genre { Id = 1, Name = "Genre" }
        };

        _bookRepositoryMock
            .Setup(r => r.GetByIdWithRelationsAsync(1))
            .ReturnsAsync(book);

        var result = await _service.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Title.Should().Be("Book");
    }

    [Fact]
    public async Task GetByIdAsync_WhenBookDoesNotExist_ShouldReturnNull()
    {
        _bookRepositoryMock
            .Setup(r => r.GetByIdWithRelationsAsync(99))
            .ReturnsAsync((Book?)null);

        var result = await _service.GetByIdAsync(99);

        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_WhenAuthorOrGenreDoesNotExist_ShouldThrowException()
    {
        var dto = new BookCreateDto("Book", 1, 1);

        _authorRepositoryMock
            .Setup(r => r.ExistsAsync(1))
            .ReturnsAsync(false);

        var act = async () => await _service.CreateAsync(dto);

        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Author not found");
    }

    [Fact]
    public async Task CreateAsync_WhenValid_ShouldCreateBook()
    {
        var dto = new BookCreateDto("Book", 1, 1);

        _authorRepositoryMock
            .Setup(r => r.ExistsAsync(1))
            .ReturnsAsync(true);

        _genreRepositoryMock
            .Setup(r => r.ExistsAsync(1))
            .ReturnsAsync(true);

        _bookRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Book>()))
            .ReturnsAsync(new Book { Id = 1, Title = "Book", AuthorId = 1, GenreId = 1 });

        _bookRepositoryMock
            .Setup(r => r.GetByIdWithRelationsAsync(1))
            .ReturnsAsync(new Book
            {
                Id = 1,
                Title = "Book",
                AuthorId = 1,
                Author = new Author { Id = 1, Name = "Author" },
                GenreId = 1,
                Genre = new Genre { Id = 1, Name = "Genre" }
            });

        var result = await _service.CreateAsync(dto);

        result.Should().NotBeNull();
        result.AuthorName.Should().Be("Author");
        result.GenreName.Should().Be("Genre");
    }

    [Fact]
    public async Task UpdateAsync_WhenBookDoesNotExist_ShouldReturnNull()
    {
        var dto = new BookUpdateDto("Updated", 1, 1);

        _bookRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Book?)null);

        var result = await _service.UpdateAsync(1, dto);

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_WhenValid_ShouldUpdateBook()
    {
        var existingBook = new Book { Id = 1, Title = "Old", AuthorId = 1, GenreId = 1 };

        var dto = new BookUpdateDto("New", 1, 1);

        _bookRepositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existingBook);

        _authorRepositoryMock
            .Setup(r => r.ExistsAsync(1))
            .ReturnsAsync(true);

        _genreRepositoryMock
            .Setup(r => r.ExistsAsync(1))
            .ReturnsAsync(true);

        _bookRepositoryMock
            .Setup(r => r.GetByIdWithRelationsAsync(1))
            .ReturnsAsync(new Book
            {
                Id = 1,
                Title = "New",
                AuthorId = 1,
                Author = new Author { Id = 1, Name = "Author" },
                GenreId = 1,
                Genre = new Genre { Id = 1, Name = "Genre" }
            });

        var result = await _service.UpdateAsync(1, dto);

        result.Should().NotBeNull();
        result!.Title.Should().Be("New");
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenBookExists()
    {
        _bookRepositoryMock
            .Setup(r => r.DeleteAsync(1))
            .ReturnsAsync(true);

        var result = await _service.DeleteAsync(1);

        result.Should().BeTrue();
    }
}
