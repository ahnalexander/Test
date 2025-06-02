using MyApp.Application.Common;

namespace MyApp.Application.Employees.Models;

public class EmployeeFilter : PaginationFilter
{
    public string? Name { get; init; }
    public int? CompanyId { get; init; }
}