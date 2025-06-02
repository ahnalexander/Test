using MyApp.API.Extensions;
using MyApp.Application.Common;
using MyApp.Application.Common.CommandHandlers;
using MyApp.Application.Common.QueryHandlers;
using MyApp.Application.Companies.Models;
using MyApp.Application.Companies.Queries;
using MyApp.Domain;
using MyApp.Application.Common.Results;

namespace MyApp.API.Endpoints;

public class CompanyEndpoints : IEndpointHandlers
{
    private const string GroupPrefix = "/api/companies";

    public void RegisterEndpoints(WebApplication app, IServiceProvider serviceProvider)
    {
        var group = app.MapGroup(GroupPrefix)
            .WithGroupName("Companies")
            .RequireAuthorization()
            ;

        group.MapGetFilteredCollection<
            GetCompaniesQuery,
            PagedResult<CompanyDto>,
            PaginationFilter>(serviceProvider, "/");

        group.MapGetItem<
            BaseGetByIdQuery<Company, CompanyDto>,
            CompanyDto>(serviceProvider, "/{id}");

        group.MapPostItem<BaseCreateCommand<Company, UpdateCompanyModel>>("/", GroupPrefix);

        group.MapPutItem<
            UpdateCompanyModel,
            BaseUpdateCommand<Company, UpdateCompanyModel>>(serviceProvider, "/{id}");

        group.MapDeleteItem<BaseDeleteCommand<Company>>(serviceProvider, "/{id}");
    }

    public void RegisterHandlers(IServiceCollection services)
    {
        services.AddGetByIdHandler<Company, CompanyDto>();
        services.AddDeleteHandler<Company>();
        services.AddUpdateHandler<Company, UpdateCompanyModel>();
        services.AddCreateHandler<Company, UpdateCompanyModel>();
    }
}