using BLL.DTO.UrlDTO;
using DAL.Entities;
using DAL.Repositories.RepositoryInterfaces;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services;

public class UrlService : IUrlService
{
    private readonly IUrlRepository _urlRepository;
    private readonly IUserRepository _userRepository;

    public UrlService(IUrlRepository urlRepository, IUserRepository userRepository)
    {
        _urlRepository = urlRepository;
        _userRepository = userRepository;
    }

    public async Task<UrlDto> CreateUrlAsync(string fullUrl, string userId)
    {
        var url = await _urlRepository.GetUrlByFullUrlAsync(fullUrl);
        var urlIsExisted = true;
        
        if (url == null)
        {
            urlIsExisted = false;
            url = new Url
            {
                Id = Guid.NewGuid().ToString(),
                FullUrl = fullUrl,
                ShortUrl = GetHash(fullUrl),
                UserId = userId,
                Date = DateTime.UtcNow
            };
            await _urlRepository.CreateUrlAsync(url);
        }
        
        return new UrlDto
        {
            Id = url.Id,
            FullUrl = url.FullUrl,
            ShortUrl = url.ShortUrl,
            Date = url.Date,
            Existed = urlIsExisted
        };
    }

    public async Task DeleteUrlAsync(string id, string userId, bool isAdmin)
    {
        var url = await _urlRepository.GetUrlByIdAsync(id);  
        if (url == null || (url.UserId != userId && !isAdmin))
        {
            throw new UnauthorizedAccessException("You are not authorized or you are not admin to delete this URL.");
        }
        await _urlRepository.DeleteUrlAsync(id);
    }

    public async Task<IEnumerable<UrlShortInfoDto>> GetAllUrlsAsync()
    {
        return (await _urlRepository.GetAllUrlsAsync()).Select(u => new UrlShortInfoDto
        {
            Id = u.Id,
            FullUrl = u.FullUrl,
            ShortUrl = u.ShortUrl,
            CreatorId = u.UserId
        });
    }

    public async Task<UrlDetailsDto> GetUrlDetailsAsync(string id)
    {
        var url = await _urlRepository.GetUrlByIdAsync(id);
        if (url == null)
        {
            throw new KeyNotFoundException("URL not found.");
        }

        return new UrlDetailsDto
        {
            Date = url.Date,
            UserLogin = url.User?.Login
        };
    }

    public async Task<string> GetFullUrlByShortUrlAsync(string shortUrl)
    {
        var url = await _urlRepository.GetUrlByShortUrlAsync(shortUrl);
        if (url == null)
        {
            throw new KeyNotFoundException("URL not found.");
        }
        return url.FullUrl;
    }
    
    private string GetHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            string base64String = Convert.ToBase64String(hashBytes);
            
            string result = base64String.Replace("/", "").Replace("+", "").Replace("=", "").Substring(0, 8);
            
            return result;
        }
    }
}