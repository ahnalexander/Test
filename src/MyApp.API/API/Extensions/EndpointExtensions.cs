using MediatR;
using MyApp.Application.Common;

namespace MyApp.API.Extensions;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder MapGetItem<TRequest, TResponse>(
        this RouteGroupBuilder group,
        IServiceProvider serviceProvider,
        string pattern)
        where TRequest : IRequest<TResponse>
    {
        return group.MapGet(pattern, async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var request = ActivatorUtilities.CreateInstance<TRequest>(serviceProvider, id);

            var result = await mediator.Send(request, cancellationToken);

            return result is not null ? Results.Ok(result) : Results.NotFound();
        });
    }

    public static RouteHandlerBuilder MapGetCollection<TRequest, TResponse>(
        this RouteGroupBuilder group,
        string pattern)
        where TRequest : IRequest<TResponse>
    {
        return group.MapGet(pattern, async (IMediator mediator, CancellationToken cancellationToken) =>
            Results.Ok(await mediator.Send(Activator.CreateInstance<TRequest>(), cancellationToken)));
    }

    public static RouteHandlerBuilder MapGetFilteredCollection<TRequest, TResponse, TFilter>(
        this RouteGroupBuilder group,
        IServiceProvider serviceProvider,
        string pattern)
        where TRequest : IRequest<TResponse>, ICollectionFilter<TFilter>
        where TFilter : PaginationFilter
    {
        return group.MapGet(pattern, async ([AsParameters] TFilter filter, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var request = ActivatorUtilities.CreateInstance<TRequest>(serviceProvider, filter);

            return Results.Ok(await mediator.Send(request, cancellationToken));
        });
    }

    public static RouteHandlerBuilder MapPostItem<TRequest>(
        this RouteGroupBuilder group,
        string pattern,
        string groupPrefix)
        where TRequest : IRequest<int>
    {
        return group.MapPost(pattern, async (TRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var id = await mediator.Send(request, cancellationToken);
            return Results.Created($"{groupPrefix}{id}", id);
        });
    }

    public static RouteHandlerBuilder MapPutItem<TRequest, TCommand>(
        this RouteGroupBuilder group,
        IServiceProvider serviceProvider,
        string pattern)
        where TCommand : IRequest<Unit>
    {
        return group.MapPut(pattern, async (int id, TRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            if (request == null)
                return Results.BadRequest("Request body cannot be null");

            var command = ActivatorUtilities.CreateInstance<TCommand>(serviceProvider, id, request);
            await mediator.Send(command, cancellationToken);
            return Results.NoContent();
        });
    }

    public static RouteHandlerBuilder MapDeleteItem<TRequest>(
        this RouteGroupBuilder group,
        IServiceProvider serviceProvider,
        string pattern)
        where TRequest : IRequest<Unit>
    {
        return group.MapDelete(pattern, async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var request = ActivatorUtilities.CreateInstance<TRequest>(serviceProvider, id);
            await mediator.Send(request, cancellationToken);
            return Results.NoContent();
        });
    }
}