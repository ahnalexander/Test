using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace MyApp.API.Endpoints;

public class DataEndpoints : IEndpoint
{
    private const string GroupPrefix = "/api/data";

    public void RegisterEndpoints(WebApplication app, IServiceProvider serviceProvider)
    {
        var group = app.MapGroup(GroupPrefix)
            .WithGroupName("Data")
            .RequireAuthorization();

        group.MapGet("/", async (IMemoryCache cache) =>
        {
            if (cache.TryGetValue("external-data", out var cachedData))
            {
                return Results.Ok(cachedData);
            }

            return Results.StatusCode(503);
        });

    }
}