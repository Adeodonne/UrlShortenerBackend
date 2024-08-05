using DAL.Entities;
using DAL.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly AppDbContext _context;

    public UrlRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Url> CreateUrlAsync(Url url)
    {
        await _context.Urls.AddAsync(url);
        await _context.SaveChangesAsync();
        return url;
    }

    public async Task<Url> GetUrlByIdAsync(string id)
    {
        return await _context.Urls
            .Include(u => u.User)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Url> GetUrlByShortUrlAsync(string shortUrl)
    {
        return await _context.Urls
            .FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
    }

    public async Task<IEnumerable<Url>> GetAllUrlsAsync()
    {
        return await _context.Urls.ToListAsync();
    }

    public async Task DeleteUrlAsync(string id)
    {
        var url = await GetUrlByIdAsync(id);
        if (url != null)
        {
            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Url> GetUrlByFullUrlAsync(string fullUrl)
    {
        return await _context.Urls
            .FirstOrDefaultAsync(u => u.FullUrl == fullUrl);
    }
}