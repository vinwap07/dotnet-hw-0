using Microsoft.AspNetCore.Mvc;

namespace Hw_2_crud_api;

[ApiController]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

    [HttpGet("api/users")]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpPost("api/users")]
    public IActionResult AddUser(string login, string password)
    {
        try
        {
            _userService.AddUser(login, password);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Created();
    }

    [HttpPut("api/users/{login}")]
    public IActionResult UpdateUser(string login, string newLogin, string newPassword)
    {
        try
        {
            _userService.UpdateUser(login, newLogin, newPassword);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok();
    }

    [HttpDelete("api/users/{login}")]
    public IActionResult DeleteUser(string login)
    {
        _userService.DeleteUser(login);
        return NoContent();
    }
}