using BLL.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConstantsController : ControllerBase
{
    private readonly IConstantService _constantService;

    public ConstantsController(IConstantService constantService)
    {
        _constantService = constantService;
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetConstantByName(string name)
    {
        var constant = await _constantService.GetConstantByNameAsync(name);
        if (constant == null)
        {
            return NotFound();
        }

        return Ok(constant);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateConstant(string name, [FromBody] string updatedValue)
    {
        try
        {
            await _constantService.UpdateConstantAsync(name, updatedValue, User.IsInRole("Admin") ? "Admin" : "User");
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}