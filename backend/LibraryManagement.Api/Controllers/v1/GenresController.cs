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
    public async Task<IActionResult> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var genre = await _service.GetByIdAsync(id);
        return genre == null ? NotFound() : Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenreCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] GenreUpdateDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var removed = await _service.DeleteAsync(id);
        return removed ? NoContent() : NotFound();
    }
}
