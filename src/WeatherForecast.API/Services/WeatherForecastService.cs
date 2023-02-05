using System.Collections.Concurrent;
namespace WeatherForecast.API.Services;

public class WeatherForecastService
{
    private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastService> _logger;

    private ConcurrentDictionary<string, Models.WeatherForecast> _state 
        = new ConcurrentDictionary<string, Models.WeatherForecast>(GetInitial());

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<Models.WeatherForecast>> GetAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        return _state.Values.AsEnumerable();
    }

    private static Dictionary<string, Models.WeatherForecast> GetInitial()
        => Enumerable.Range(1, 5).Select(index => new Models.WeatherForecast
        {
            Id = Guid.NewGuid().ToString(),
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToDictionary(x => x.Id ?? string.Empty);
}
