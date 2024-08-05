using BLL.DTO.UrlDTO;

namespace BLL.Services;

public interface IUrlService
{
    Task<UrlDto> CreateUrlAsync(string fullUrl, string userId);
    Task DeleteUrlAsync(string id, string userId, bool isAdmin);
    Task<IEnumerable<UrlShortInfoDto>> GetAllUrlsAsync();
    Task<UrlDetailsDto> GetUrlDetailsAsync(string id);
    Task<string> GetFullUrlByShortUrlAsync(string shortUrl);
}