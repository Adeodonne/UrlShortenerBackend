using System.Security.Claims;
using BLL.DTO.UrlDTO;
using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;

    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateUrl([FromBody] CreateUrlRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _urlService.CreateUrlAsync(request.FullUrl, userId);
        return CreatedAtAction(nameof(GetUrlDetails), new { id = result.Id }, result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUrl(string id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _urlService.DeleteUrlAsync(id, userId, User.IsInRole("Admin"));
        return NoContent();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUrls()
    {
        var urls = await _urlService.GetAllUrlsAsync();
        return Ok(urls);
    }

    [HttpGet("getDetails/{id}")]
    public async Task<IActionResult> GetUrlDetails(string id)
    {
        try
        {
            var details = await _urlService.GetUrlDetailsAsync(id);
            return Ok(details);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("getFullUrl/{shortUrl}")]
    public async Task<IActionResult> GetFullUrlByShortUrl(string shortUrl)
    {
        try
        {
            var fullUrl = await _urlService.GetFullUrlByShortUrlAsync(shortUrl);
            return Ok(new { FullUrl = fullUrl });
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}