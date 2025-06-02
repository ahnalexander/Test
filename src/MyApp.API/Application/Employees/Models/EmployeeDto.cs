namespace MyApp.Application.Employees.Models;

public class EmployeeDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int CompanyId { get; set; }
    public required string Email { get; set; }
}
