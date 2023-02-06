using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecast.API.Services;

public class WeatherForecastService
{
    private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly GuidGenerationService _guidService;
    private readonly ILogger<WeatherForecastService> _logger;

    private readonly ConcurrentDictionary<string, Models.WeatherForecast> _state;

    public WeatherForecastService(GuidGenerationService guidService, ILogger<WeatherForecastService> logger)
    {
        _guidService = guidService;
        _logger = logger;
        _state = new ConcurrentDictionary<string, Models.WeatherForecast>(GetInitial());
    }

    public async Task<IEnumerable<Models.WeatherForecast>> GetAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        return _state.Values.AsEnumerable();
    }

    public async Task<Models.WeatherForecast?> FindAsync(string id, CancellationToken cancellationToken)
    {
        await Task.Delay(200, cancellationToken);
        
        _state.TryGetValue(id, out var result);

        return result;
    }

    public async Task<string> AddAsync(CancellationToken cancellationToken)
    {
        return await Task.Run(() => {
            var newId = _guidService.New().ToString();
            var @new = new Models.WeatherForecast()
            {
                Id = newId,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(_state.Count)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            };

            _state.TryAdd(newId, @new);

            return newId;

        }, cancellationToken);  
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await Task.Run(() => {

            _state.TryRemove(id, out var _);
        
        }, cancellationToken);
    }

    public async Task<Models.WeatherForecast> UpdateAsync(string id, CancellationToken cancellationToken)
    {
        return await Task.Run(() => {
            if (!_state.TryGetValue(id, out var previous))
                throw new InvalidOperationException($"id={id} not found");

            var update =  new Models.WeatherForecast()
            {
                Id = id,
                Date = previous.Date,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            };

            _state.TryUpdate(id, update, previous);

            return update;      
            
        }, cancellationToken);
    }

    private Dictionary<string, Models.WeatherForecast> GetInitial()
        => Enumerable.Range(1, 5).Select(index => new Models.WeatherForecast
        {
            Id = _guidService.New().ToString(),
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToDictionary(x => x.Id ?? string.Empty);
}
