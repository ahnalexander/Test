using MediatR;
using AutoMapper;
using MyApp.Infrastructure;
using MyApp.Domain;
using MyApp.Application.Common.QueryHandlers;
using MyApp.Application.Companies.Models;
using MyApp.Application.Common.Results;
using MyApp.Application.Common;

namespace MyApp.Application.Companies.Queries;

public class GetCompaniesQueryHandler 
    : BaseGetCollectionQueryHandler<Company, CompanyDto, GetCompaniesQuery, PaginationFilter>,
    IRequestHandler<GetCompaniesQuery, PagedResult<CompanyDto>>
{
    public GetCompaniesQueryHandler(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<PagedResult<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        => await BaseHandle(request, cancellationToken);
}
