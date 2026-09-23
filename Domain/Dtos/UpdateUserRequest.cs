using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos;

public class UpdateUserRequest
{
    [Required]
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string? Login { get; set; }
    
    [MaxLength(64)]
    public string? NewPassword { get; set; }
}