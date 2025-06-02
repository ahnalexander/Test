using MyApp.API.Extensions;
using MyApp.Application.Common.CommandHandlers;
using MyApp.Application.Common.QueryHandlers;
using MyApp.Application.Common.Results;
using MyApp.Application.Employees.Models;
using MyApp.Application.Employees.Queries;
using MyApp.Domain;

namespace MyApp.API.Endpoints;

public class EmployeeEndpoints : IEndpointHandlers
{
    private const string GroupPrefix = "/api/employees";

    public void RegisterEndpoints(WebApplication app, IServiceProvider serviceProvider)
    {
        var group = app.MapGroup(GroupPrefix)
            .WithGroupName("Employees")
            .RequireAuthorization()
            ;

        group.MapGetFilteredCollection<
            GetEmployeesQuery,
            PagedResult<EmployeeDto>,
            EmployeeFilter>(serviceProvider, "/");

        group.MapGetItem<
            BaseGetByIdQuery<Employee, EmployeeDto>,
            EmployeeDto>(serviceProvider, "/{id}");

        group.MapPostItem<BaseCreateCommand<Employee, UpdateEmployeeModel>>("/", GroupPrefix);

        group.MapPutItem<
            UpdateEmployeeModel,
            BaseUpdateCommand<Employee, UpdateEmployeeModel>>(serviceProvider, "/{id}");

        group.MapDeleteItem<BaseDeleteCommand<Employee>>(serviceProvider, "/{id}");
    }

    public void RegisterHandlers(IServiceCollection services)
    {
        services.AddGetByIdHandler<Employee, EmployeeDto>();
        services.AddDeleteHandler<Employee>();
        services.AddUpdateHandler<Employee, UpdateEmployeeModel>();
        services.AddCreateHandler<Employee, UpdateEmployeeModel>();
    }
}