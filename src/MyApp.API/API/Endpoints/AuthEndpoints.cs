using MediatR;
using MyApp.Application.Auth.Commands;
using MyApp.Application.Auth.Models;

namespace MyApp.API.Endpoints;

public class AuthEndpoints : IEndpoint
{
    public void RegisterEndpoints(WebApplication app, IServiceProvider serviceProvider)
    {
        var group = app.MapGroup("/api/auth")
            .WithGroupName("Auth");

        group.MapPost("/login", async (LoginDto loginDto, IMediator mediator) =>
        {
            var command = new LoginCommand(loginDto.Username, loginDto.Password);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("Login")
        .AllowAnonymous();
    }
}