using AutoMapper;
using MediatR;
using MyApp.Application.Common.Filtering;
using MyApp.Application.Common.QueryHandlers;
using MyApp.Application.Common.Results;
using MyApp.Application.Employees.Models;
using MyApp.Domain;
using MyApp.Infrastructure;

namespace MyApp.Application.Employees.Queries;

public class GetEmployeesQueryHandler : BaseGetCollectionQueryHandler<Employee, EmployeeDto, GetEmployeesQuery, EmployeeFilter>,
    IRequestHandler<GetEmployeesQuery, PagedResult<EmployeeDto>>
{
    public GetEmployeesQueryHandler(
        ApplicationDbContext context,
        IMapper mapper,
        IFilterCollection<Employee, EmployeeFilter> filter) : base(context, mapper, filter)
    {
    }

    public Task<PagedResult<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        => BaseHandle(request, cancellationToken);
}
