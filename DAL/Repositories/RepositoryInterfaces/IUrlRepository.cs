using DAL.Entities;

namespace DAL.Repositories.RepositoryInterfaces;

public interface IUrlRepository
{
    Task<Url> CreateUrlAsync(Url url);
    Task<Url> GetUrlByIdAsync(string id);
    Task<Url> GetUrlByShortUrlAsync(string shortUrl);
    Task<IEnumerable<Url>> GetAllUrlsAsync();
    Task DeleteUrlAsync(string id);
    Task<Url> GetUrlByFullUrlAsync(string fullUrl);

}