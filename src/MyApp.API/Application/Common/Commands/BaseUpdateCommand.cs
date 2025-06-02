using MediatR;
using MyApp.Domain;

namespace MyApp.Application.Common.CommandHandlers;

public record BaseUpdateCommand<TEntity, TRequest>(int Id, TRequest Data)
    : IRequest<Unit>
    where TEntity : BaseEntity;
