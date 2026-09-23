using Domain.Dtos;
using Domain.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("api/login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        
        try
        {
            var user = await authService.LoginAsync(command, cancellationToken);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            };
            
            Response.Cookies.Append("user-id", user.Id.ToString(), cookieOptions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok();
    }
}