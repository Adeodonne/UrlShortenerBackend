using BLL.DTO.UserDTO;
using BLL.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateLoginDTO userDto)
    {
        try
        {
            await _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(_userService.GetUserByLoginAndPassword), new {request = userDto}, userDto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> GetUserByLoginAndPassword([FromBody] UserCreateLoginDTO request)
    {
        try
        {
            var user = await _userService.GetUserByLoginAndPassword(request.Login, request.Password);
            return Ok(user);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
