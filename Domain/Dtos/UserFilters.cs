namespace Domain.Dtos;

public class UserFilters
{
    public DateTimeOffset? CreatedAtFrom { get; set; }
    public DateTimeOffset? CreatedAtTo { get; set; }
    public DateTimeOffset? UpdatedAtFrom { get; set; }
    public DateTimeOffset? UpdatedAtTo { get; set; }
}