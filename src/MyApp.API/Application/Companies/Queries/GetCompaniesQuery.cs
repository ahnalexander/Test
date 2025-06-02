using MediatR;
using MyApp.Application.Common;
using MyApp.Application.Companies.Models;
using MyApp.Application.Common.Results;

namespace MyApp.Application.Companies.Queries;

public record GetCompaniesQuery : IRequest<PagedResult<CompanyDto>>, ICollectionFilter<PaginationFilter>
{
    public GetCompaniesQuery(PaginationFilter pagination)
    {
        FilterData = pagination;
    }

    public PaginationFilter FilterData { get; init; }
}
