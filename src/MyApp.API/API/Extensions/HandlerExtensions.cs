using MediatR;
using MyApp.Application.Common.CommandHandlers;
using MyApp.Application.Common.QueryHandlers;
using MyApp.Domain;

namespace MyApp.API.Extensions;

public static class HandlerExtensions
{
    public static IServiceCollection AddGetByIdHandler<TEntity, TDto>(this IServiceCollection services)
        where TEntity : BaseEntity
        where TDto : class
    {
        services.AddTransient(
            typeof(IRequestHandler<BaseGetByIdQuery<TEntity, TDto>, TDto>),
            typeof(BaseGetByIdQueryHandler<TEntity, TDto>));

        return services;
    }

    public static IServiceCollection AddDeleteHandler<TEntity>(this IServiceCollection services)
        where TEntity : BaseEntity
    {
        services.AddTransient(
            typeof(IRequestHandler<BaseDeleteCommand<TEntity>, Unit>),
            typeof(BaseDeleteCommandHandler<TEntity>));

        return services;
    }

    public static IServiceCollection AddUpdateHandler<TEntity, TDto>(this IServiceCollection services)
        where TEntity : BaseEntity
        where TDto : class
    {
        services.AddTransient(
            typeof(IRequestHandler<BaseUpdateCommand<TEntity, TDto>, Unit>),
            typeof(BaseUpdateCommandHandler<TEntity, TDto>));

        return services;
    }

    public static IServiceCollection AddCreateHandler<TEntity, TDto>(this IServiceCollection services)
        where TEntity : BaseEntity
        where TDto : class
    {
        services.AddTransient(
            typeof(IRequestHandler<BaseCreateCommand<TEntity, TDto>, int>),
            typeof(BaseCreateCommandHandler<TEntity, TDto>));

        return services;
    }
}