using MyApp.Application.Common.Filtering;
using MyApp.Application.Employees.Models;
using MyApp.Domain;

namespace MyApp.API.Extensions;

public static class FilteringExtensions
{
    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        return services.AddScoped<IFilterCollection<Employee, EmployeeFilter>, BaseFilterCollection<Employee, EmployeeFilter>>();
    }
}