using System.Collections;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NOS.Engineering.Challenge.API.Models;
using NOS.Engineering.Challenge.Database;
using NOS.Engineering.Challenge.Managers;
using NOS.Engineering.Challenge.Models;

namespace NOS.Engineering.Challenge.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
    private readonly IContentsManager _manager;
    public ContentController(IContentsManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public async Task<IActionResult> GetManyContents()
    {
        var contents = await _manager.GetManyContents().ConfigureAwait(false);

        if (!contents.Any())
            return NotFound();

        return Ok(contents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContent(Guid id)
    {
        var content = await _manager.GetContent(id).ConfigureAwait(false);

        if (content == null)
            return NotFound();

        return Ok(content);
    }

    [HttpPost]
    public async Task<IActionResult> CreateContent(
        [FromBody] ContentInput content
        )
    {
        var createdContent = await _manager.CreateContent(content.ToDto()).ConfigureAwait(false);

        return createdContent == null ? Problem() : Ok(createdContent);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateContent(
        Guid id,
        [FromBody] ContentInput content
        )
    {
        var updatedContent = await _manager.UpdateContent(id, content.ToDto()).ConfigureAwait(false);

        return updatedContent == null ? NotFound() : Ok(updatedContent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContent(
        Guid id
    )
    {
        var deletedId = await _manager.DeleteContent(id).ConfigureAwait(false);
        return Ok(deletedId);
    }

    [HttpPost("{id}/genre")]
    public async Task<IActionResult> AddGenres(
        Guid id,
        [FromBody] IEnumerable<string> genre
    )
    {

        var content = await _manager.GetContent(id).ConfigureAwait(false);

        if (content == null || genre == null)
        {
            Console.WriteLine("passei aqui");
            return NotFound();
        }

        List<string> genres = content.GenreList.ToList();
        List<string> genreList = genre.ToList();
        List<string> filteredItems = genreList.Where(x => genres.All(y => !y.Equals(x))).ToList();

        if (filteredItems.Count == 0)
        {
            return NotFound();
        }

        foreach (var item in filteredItems)
        {
            genres.Add(item);
        }

        ContentDto newData = new(
            content.Title,
            content.SubTitle,
            content.Description,
            content.ImageUrl,
            content.Duration,
            content.StartTime,
            content.EndTime,
            genres
        );

        var updatedContent = await _manager.UpdateContent(id, newData).ConfigureAwait(false);

        return updatedContent == null ? NotFound() : Ok(updatedContent);
    }

    [HttpDelete("{id}/genre")]
    public async Task<IActionResult> RemoveGenres(
        Guid id,
        [FromBody] IEnumerable<string> genre
    )
    {

        var content = await _manager.GetContent(id).ConfigureAwait(false);

        if (content == null || genre == null)
        {
            return NotFound();
        }


        List<string> genres = content.GenreList.ToList();
        List<string> genreList = genre.ToList();
        List<string> filteredItems = genres.Where(x => genreList.All(y => !y.Equals(x))).ToList();

        ContentDto data = new(
            content.Title,
            content.SubTitle,
            content.Description,
            content.ImageUrl,
            content.Duration,
            content.StartTime,
            content.EndTime,
            filteredItems
        );

        var updatedContent = await _manager.UpdateContent(id, data).ConfigureAwait(false);

        return updatedContent == null ? NotFound() : Ok(updatedContent);
    }
}