using AutoMapper;
using MediatR;
using MyApp.Domain;
using MyApp.Infrastructure;
using MyApp.Application.Common.Exceptions;

namespace MyApp.Application.Common.CommandHandlers;

public class BaseUpdateCommandHandler<TEntity, TRequest>(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<BaseUpdateCommand<TEntity, TRequest>, Unit>
    where TEntity : BaseEntity
{
    public async Task<Unit> Handle(BaseUpdateCommand<TEntity, TRequest> request, CancellationToken cancellationToken)
    {
        var entity = await context.Set<TEntity>().FindAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(typeof(TEntity).Name, request.Id);

        mapper.Map(request.Data, entity);

        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}