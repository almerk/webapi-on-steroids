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

    public async Task<Models.WeatherForecast> GetAsync(string id, CancellationToken cancellationToken)
    {
        await Task.Delay(200, cancellationToken);
        if (!_state.TryGetValue(id, out var result))
            throw new InvalidOperationException($"id={id} not found");//TODO: validation
        
        return result;
    }

    public async Task<string> AddAsync(CancellationToken cancellationToken)
    {
        return await Task.Run(() => {
            var newId = Guid.NewGuid().ToString();
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
                throw new InvalidOperationException($"id={id} not found"); //TODO: validation

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
