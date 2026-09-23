using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

    [HttpGet("api/users")]
    public async Task<IActionResult> GetUsers([FromQuery] UserFilters userFilters, CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetUsers(userFilters, cancellationToken);
        return Ok(users);
    }

    [HttpPost("api/users")]
    public async Task<IActionResult> AddUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await _userService.AddUser(request, cancellationToken);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Created();
    }

    [HttpPut("api/users/{login}")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await _userService.UpdateUser(request, cancellationToken);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok();
    }

    [HttpDelete("api/users/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken = default)
    {
        await _userService.DeleteUser(id, cancellationToken);
        return NoContent();
    }
}