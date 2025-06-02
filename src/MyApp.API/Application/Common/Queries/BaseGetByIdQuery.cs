using MediatR;

namespace MyApp.Application.Common.QueryHandlers;

public record BaseGetByIdQuery<TEntity, TDto>(int Id) : IRequest<TDto>
    where TEntity : class
    where TDto : class;