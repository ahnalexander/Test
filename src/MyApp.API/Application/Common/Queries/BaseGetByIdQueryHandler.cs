using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Common.Exceptions;
using MyApp.Domain;
using MyApp.Infrastructure;

namespace MyApp.Application.Common.QueryHandlers;

public class BaseGetByIdQueryHandler<TEntity, TDto>(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<BaseGetByIdQuery<TEntity, TDto>, TDto>
    where TEntity : BaseEntity
    where TDto : class
{
    public async Task<TDto> Handle(BaseGetByIdQuery<TEntity, TDto> request, CancellationToken cancellationToken)
    {
        var entity = await context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException(typeof(TEntity).Name, request.Id);

        return mapper.Map<TDto>(entity);
    }
}
