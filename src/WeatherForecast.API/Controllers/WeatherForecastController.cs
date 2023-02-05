using Microsoft.AspNetCore.Mvc;
using WeatherForecast.API.Services;

namespace WeatherForecast.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastService _service;
    public WeatherForecastController(WeatherForecastService service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetWeatherForecasts")]
    public async Task<IEnumerable<Models.WeatherForecast>> GetAsync(CancellationToken cancellationToken)
    {
       return await _service.GetAsync(cancellationToken);
    }

    [HttpGet("{id}", Name = "GetWeatherForecast")]
    public async Task<Models.WeatherForecast> GetAsync(string id, CancellationToken cancellationToken)
    {
       return await _service.GetAsync(id, cancellationToken);
    }

    [HttpPost(Name = "AddWeatherForecast")]
    public async Task<string> AddAsync(CancellationToken cancellationToken)
    {
        return await _service.AddAsync(cancellationToken);
    }

    [HttpPut("{id}", Name = "UpdateWeatherForecastFull")]
    public async Task<Models.WeatherForecast> UpdateAsync(string id, CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(id, cancellationToken);
    }

    [HttpDelete("{id}", Name = "DeleteWeatherForecast")]
    public async Task UdpateAsync(string id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
    } 
}
