using DAL.Entities;

namespace DAL.Repositories.RepositoryInterfaces;

public interface IConstantRepository
{
    Task<Constant> GetConstantByNameAsync(string name);
    Task UpdateConstantAsync(Constant constant);
}