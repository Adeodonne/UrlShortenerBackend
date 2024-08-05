using BLL.DTO.UserDTO;
using DAL.Entities;

namespace BLL.Services.ServicesInterfaces;

public interface IUserService
{
    Task<GetUserDTO> GetUserByLoginAndPassword(string login, string password);
    Task CreateUser(UserCreateLoginDTO userDTO);
}