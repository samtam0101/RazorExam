namespace Domain.Filters;

public class CustomerFilter:PaginationFilter
{
    public string Name { get; set; }
    public string Email { get; set; }
}
