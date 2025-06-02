using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Common.Exceptions;
using MyApp.Domain;
using MyApp.Infrastructure;

namespace MyApp.Application.Common.CommandHandlers;

public class BaseDeleteCommandHandler<TEntity>(ApplicationDbContext context)
    : IRequestHandler<BaseDeleteCommand<TEntity>, Unit>
    where TEntity : BaseEntity
{

    public async Task<Unit> Handle(BaseDeleteCommand<TEntity> request, CancellationToken cancellationToken)
    {
        var entity = await context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity == null)
            throw new NotFoundException(typeof(TEntity).Name, request.Id);

        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}