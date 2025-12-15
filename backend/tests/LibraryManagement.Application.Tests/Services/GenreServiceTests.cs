using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LibraryManagement.Application.DTOs.Genre;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using Moq;
using Xunit;

namespace LibraryManagement.Application.Tests.Services;

public class GenreServiceTests
{
    private readonly Mock<IGenreRepository> _repositoryMock;
    private readonly GenreService _service;

    public GenreServiceTests()
    {
        _repositoryMock = new Mock<IGenreRepository>();
        _service = new GenreService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfGenres()
    {
        var genres = new List<Genre>
        {
            new() { Id = 1, Name = "Mystery" },
            new() { Id = 2, Name = "Fantasy" }
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(genres);

        var result = await _service.GetAllAsync();

        result.Should().HaveCount(2);
        result.First().Name.Should().Be("Mystery");
    }

    [Fact]
    public async Task GetByIdAsync_WhenGenreExists_ShouldReturnGenre()
    {
        var genre = new Genre { Id = 1, Name = "Mystery" };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(genre);

        var result = await _service.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Mystery");
    }

    [Fact]
    public async Task GetByIdAsync_WhenGenreDoesNotExist_ShouldReturnNull()
    {
        _repositoryMock
            .Setup(r => r.GetByIdAsync(99))
            .ReturnsAsync((Genre?)null);

        var result = await _service.GetByIdAsync(99);

        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateGenre()
    {
        var dto = new GenreCreateDto("Sci-Fi");

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Genre>()))
            .ReturnsAsync(new Genre { Id = 1, Name = "Sci-Fi" });

        var result = await _service.CreateAsync(dto);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Sci-Fi");

        _repositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Genre>()),
            Times.Once
        );
    }

    [Fact]
    public async Task UpdateAsync_WhenGenreExists_ShouldUpdateAndReturnGenre()
    {
        var existingGenre = new Genre { Id = 1, Name = "Old Name" };
        var dto = new GenreUpdateDto("New Name");

        _repositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(existingGenre);

        var result = await _service.UpdateAsync(1, dto);

        result.Should().NotBeNull();
        result!.Name.Should().Be("New Name");

        _repositoryMock.Verify(r => r.UpdateAsync(existingGenre), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenGenreDoesNotExist_ShouldReturnNull()
    {
        var dto = new GenreUpdateDto("New Name");

        _repositoryMock
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((Genre?)null);

        var result = await _service.UpdateAsync(1, dto);

        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_WhenGenreExists_ShouldReturnTrue()
    {
        _repositoryMock
            .Setup(r => r.DeleteAsync(1))
            .ReturnsAsync(true);

        var result = await _service.DeleteAsync(1);

        result.Should().BeTrue();
    }
}
