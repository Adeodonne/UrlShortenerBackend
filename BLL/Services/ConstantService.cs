using BLL.Services.ServicesInterfaces;
using DAL.Entities;
using DAL.Repositories.RepositoryInterfaces;

namespace BLL.Services;

public class ConstantService : IConstantService
{
    private readonly IConstantRepository _constantRepository;

    public ConstantService(IConstantRepository constantRepository)
    {
        _constantRepository = constantRepository;
    }

    public async Task<Constant> GetConstantByNameAsync(string name)
    {
        return await _constantRepository.GetConstantByNameAsync(name);
    }

    public async Task UpdateConstantAsync(string name, string value, string userRole)
    {
        if (userRole != "Admin")
        {
            throw new UnauthorizedAccessException("Only admins can edit constants.");
        }
        
        var constant = await GetConstantByNameAsync(name);

        if (constant == null)
        {
            throw new KeyNotFoundException();
        }

        constant.Value = value;

        await _constantRepository.UpdateConstantAsync(constant);
    }
}