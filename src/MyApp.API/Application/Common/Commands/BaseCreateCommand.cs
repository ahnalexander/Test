using MediatR;
using MyApp.Domain;

namespace MyApp.Application.Common.CommandHandlers;

public record BaseCreateCommand<TEntity, TRequest>(TRequest Data) : IRequest<int>
    where TEntity : BaseEntity;
