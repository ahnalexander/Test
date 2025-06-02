namespace MyApp.API.Endpoints;

public interface IEndpointRegistration
{
    void RegisterEndpoints(WebApplication app, IServiceProvider serviceProvider);
}

public interface IHandlerRegistration
{
    void RegisterHandlers(IServiceCollection services);
}

public interface IEndpoint : IEndpointRegistration { }

public interface IEndpointHandlers : IEndpointRegistration, IHandlerRegistration { }