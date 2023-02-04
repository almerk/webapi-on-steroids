using System.Collections.Concurrent;

namespace WeatherForecast.API.Services;

public class WeatherForecastService
{
    private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastService> _logger;

    private ConcurrentDictionary<string, WeatherForecast> _state = new ConcurrentDictionary<string, WeatherForecast>(GetInitial());

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }

    public IEnumerable<WeatherForecast> Get()
    {
        return _state.Values;
    }

    private static Dictionary<string, WeatherForecast> GetInitial()
        => Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Id = Guid.NewGuid().ToString(),
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToDictionary(x => x.Id ?? string.Empty);
}
