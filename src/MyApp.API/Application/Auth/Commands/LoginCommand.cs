using MediatR;
using MyApp.Application.Auth.Models;

namespace MyApp.Application.Auth.Commands;

public record LoginCommand(string Username, string Password) : IRequest<AuthResponse>;