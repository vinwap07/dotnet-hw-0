using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class CreateUserRequest
{
    [Required]
    [MaxLength(128)]
    public string Login { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(64)]
    public string Password { get; set; } = string.Empty;
}