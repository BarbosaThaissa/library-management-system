using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LibraryManagement.Application.DTOs.Author;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using Moq;
using Xunit;

namespace LibraryManagement.Application.Tests.Services;

public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> _repositoryMock;
    private readonly AuthorService _service;

    public AuthorServiceTests()
    {
        _repositoryMock = new Mock<IAuthorRepository>();
        _service = new AuthorService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfAuthors()
    {
        var authors = new List<Author>
        {
            new() { Id = 1, Name = "Author 1" },
            new() { Id = 2, Name = "Author 2" }
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(authors);

        var result = await _service.GetAllAsync();

        result.Should().HaveCount(2);
        result.First().Name.Should().Be("Author 1");
    }

    [Fact]
    public async Task GetByIdAsync_WhenAuthorExists_ShouldReturnAuthor()
    {
        var author = new Author { Id = 1, Name = "Sara Shepard" };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(author);

        var result = await _service.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Sara Shepard");
    }

    [Fact]
    public async Task GetByIdAsync_WhenAuthorDoesNotExist_ShouldReturnNull()
    {
        _repositoryMock
            .Setup(r => r.GetByIdAsync(99))
            .ReturnsAsync((Author?)null);

        var result = await _service.GetByIdAsync(99);

        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateAuthor()
    {
        var dto = new AuthorCreateDto("New Author");

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Author>()))
            .ReturnsAsync(new Author { Id = 1, Name = "New Author" });

        var result = await _service.CreateAsync(dto);

        result.Should().NotBeNull();
        result.Name.Should().Be("New Author");
        result.Id.Should().Be(1);
    }

    [Fact]
    public async Task DeleteAsync_WhenAuthorExists_ShouldReturnTrue()
    {
        _repositoryMock
            .Setup(r => r.DeleteAsync(1))
            .ReturnsAsync(true);

        var result = await _service.DeleteAsync(1);

        result.Should().BeTrue();
    }
}
