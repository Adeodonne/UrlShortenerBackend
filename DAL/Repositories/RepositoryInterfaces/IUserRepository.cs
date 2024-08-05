using DAL.Entities;

namespace DAL.Repositories.RepositoryInterfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<User> GetUserByLoginAsync(string login);
    Task CreateUserAsync(User user);
    Task SaveChangesAsync();
    Task<bool> IsLoginUsedAsync(string login);
}
