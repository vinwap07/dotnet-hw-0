namespace Domain.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string UpdatedAt { get; set; } = string.Empty;
}