using DAL.Entities;
using DAL.Repositories.RepositoryInterfaces;

namespace DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByLoginAsync(string login)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Login == login);
    }
    
    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
    
    public async Task<bool> IsLoginUsedAsync(string login)
    {
        return await _context.Users.AnyAsync(u => u.Login == login);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
