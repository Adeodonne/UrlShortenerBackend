using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.DTO.UserDTO;
using BLL.Services.ServicesInterfaces;
using DAL.Entities;
using DAL.Repositories.RepositoryInterfaces;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly string _jwtSecret;

    public UserService(IUserRepository userRepository, string jwtSecret)
    {
        _userRepository = userRepository;
        _jwtSecret = jwtSecret;
    }

    public async Task<GetUserDTO> GetUserByLoginAndPassword(string login, string password)
    {
        var user = await _userRepository.GetUserByLoginAsync(login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid login or password.");
        }

        var token = GenerateJwtToken(user);
        
        GetUserDTO userDto = new GetUserDTO
        {
            Id = user.Id,
            Login = login,
            IsAdmin = user.IsAdmin,
            Token = token
        };
        
        return userDto;
    }
    
    public async Task CreateUser(UserCreateLoginDTO userDTO)
    {
        if (await _userRepository.IsLoginUsedAsync(userDTO.Login))
        {
            throw new InvalidOperationException("Login is already used.");
        }

        User user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Login = userDTO.Login,
            Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
            IsAdmin = false
        };
        
        await _userRepository.CreateUserAsync(user);
        await _userRepository.SaveChangesAsync();
    }
    
    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.Login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}
    