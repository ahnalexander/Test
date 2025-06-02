using AutoMapper;
using MediatR;
using MyApp.Domain;
using MyApp.Infrastructure;

namespace MyApp.Application.Common.CommandHandlers;

public class BaseCreateCommandHandler<TEntity, TRequest>(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<BaseCreateCommand<TEntity, TRequest>, int>
    where TEntity : BaseEntity
{
    public async Task<int> Handle(BaseCreateCommand<TEntity, TRequest> request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<TEntity>(request.Data);

        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}