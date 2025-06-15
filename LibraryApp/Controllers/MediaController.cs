using System.Text.Json;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("media")]
public class MediaController : ControllerBase
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    [HttpPost]
    public async Task<ActionResult<Media>> CreateMedia([FromBody] JsonElement mediaJson)
    {
        try
        {
            var media = MediaFactory.CreateFromJson(mediaJson);
            var addedMedia = await _mediaService.AddMediaAsync(media);
            return CreatedAtAction(nameof(GetMedia), new { id = addedMedia.Id }, addedMedia);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating media: {ex.Message}");
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Media>> GetMedia(Guid id)
    {
        try
        {
            var media = await _mediaService.GetMediaByIdAsync(id);
            if (media == null)
                return NotFound();

            return Ok(media);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]                                    // GET /media
    public async Task<ActionResult<IEnumerable<Media>>> GetAllMedia(
    [FromQuery] string? query,                   // Optional search
    [FromQuery] MediaType? mediaType)             // Optional type filter
    {
        try
        {
            if (!string.IsNullOrEmpty(query))
                return Ok(await _mediaService.SearchMediaAsync(query, mediaType));

            if (mediaType.HasValue)
                return Ok(await _mediaService.GetMediaByTypeAsync(mediaType.Value));

            return Ok(await _mediaService.GetAllMediaAsync());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMedia(Guid id)
    {
        try
        {
            var success = await _mediaService.DeleteMediaAsync(id);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"Media with ID {id} not found.");
            }
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


}