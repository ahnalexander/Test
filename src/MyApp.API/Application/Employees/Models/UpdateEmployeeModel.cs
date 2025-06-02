namespace MyApp.Application.Employees.Models;

public record UpdateEmployeeModel
{
    public required string Name { get; init; }
    public int CompanyId { get; init; }
    public required string Email { get; init; }
}
