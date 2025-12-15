using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.DTOs.Genre;
using LibraryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
public class GenresController(IGenreService service) : ControllerBase
{
    private readonly IGenreService _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GenreResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GenreResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var genre = await _service.GetByIdAsync(id);
        return genre == null ? NotFound() : Ok(genre);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenreResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] GenreCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GenreResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] GenreUpdateDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var removed = await _service.DeleteAsync(id);
        return removed ? NoContent() : NotFound();
    }
}
