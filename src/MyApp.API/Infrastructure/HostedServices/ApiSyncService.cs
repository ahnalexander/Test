using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Polly.CircuitBreaker;

internal class ApiSyncService(IHttpClientFactory httpClientFactory,
    IMemoryCache cache,
    ILogger<ApiSyncService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Sync");

            try
            {
                var client = httpClientFactory.CreateClient("TestApi");
                var response = await client.GetAsync("coffee/hot", stoppingToken);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(stoppingToken);

                    cache.Set("external-data", content, TimeSpan.FromMinutes(2));

                    logger.LogInformation("Data added in cache");
                }
                else
                {
                    logger.LogWarning("API response: {Status}", response.StatusCode);
                }
            }
            catch (BrokenCircuitException)
            {
                logger.LogWarning("Circuit breaker skip");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}