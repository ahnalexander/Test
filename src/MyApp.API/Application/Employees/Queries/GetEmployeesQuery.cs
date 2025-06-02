using MediatR;
using MyApp.Application.Common;
using MyApp.Application.Common.Results;
using MyApp.Application.Employees.Models;

namespace MyApp.Application.Employees.Queries;

public record GetEmployeesQuery : IRequest<PagedResult<EmployeeDto>>, ICollectionFilter<EmployeeFilter>
{
    public GetEmployeesQuery(EmployeeFilter filter)
    {
        FilterData = filter;
    }

    public EmployeeFilter FilterData { get; init; }
}
