using DAL.Entities;

namespace BLL.Services.ServicesInterfaces;

public interface IConstantService
{
    Task<Constant> GetConstantByNameAsync(string name);
    Task UpdateConstantAsync(string name, string value, string userRole);
}