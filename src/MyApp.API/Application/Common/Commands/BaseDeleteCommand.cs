using MediatR;
using MyApp.Domain;

namespace MyApp.Application.Common.CommandHandlers;

public record BaseDeleteCommand<TEntity>(int Id) : IRequest<Unit>
    where TEntity : BaseEntity;