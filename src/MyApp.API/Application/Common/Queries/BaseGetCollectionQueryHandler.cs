using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain;
using MyApp.Infrastructure;
using MyApp.Application.Common.Filtering;
using MyApp.Application.Common.Results;
using MediatR;

namespace MyApp.Application.Common.QueryHandlers;

public abstract class BaseGetCollectionQueryHandler<TEntity, TDto, TRequest, TFilter>
    where TEntity : BaseEntity
    where TDto : class
    where TRequest : IRequest<PagedResult<TDto>>
    where TFilter : PaginationFilter
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFilterCollection<TEntity, TFilter>? _filter;

    protected BaseGetCollectionQueryHandler(
        ApplicationDbContext context,
        IMapper mapper,
        IFilterCollection<TEntity, TFilter>? filter = null)
    {
        _context = context;
        _mapper = mapper;
        _filter = filter;
    }

    protected async Task<PagedResult<TDto>> BaseHandle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (request is not ICollectionFilter<TFilter> filter)
            {
                return PagedResult<TDto>.Create(
                    await query.Select(e => _mapper.Map<TDto>(e)).ToListAsync(cancellationToken),
                    await query.CountAsync(cancellationToken),
                    1,
                    int.MaxValue);
            }

            if (_filter != null)
            {
                query = _filter.ApplyFilter(query, filter.FilterData);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            query = ApplyPagination(query, filter.FilterData);

            var entities = await query.ToListAsync(cancellationToken);
            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);

            return PagedResult<TDto>.Create(
                dtos,
                totalCount,
                filter.FilterData.PageNumber,
                filter.FilterData.PageSize);
        }
        catch (Exception ex)
        {
            return PagedResult<TDto>.Fail($"Error retrieving collection: {ex.Message}");
        }
    }

    private static IQueryable<TEntity> ApplyPagination(IQueryable<TEntity> query, TFilter filter)
    {
        return query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);
    }
}