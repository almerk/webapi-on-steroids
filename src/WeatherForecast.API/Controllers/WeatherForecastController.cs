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

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<Models.WeatherForecast>> GetAsync(CancellationToken cancellationToken)
    {
       return await _service.GetAsync(cancellationToken);
    }
}
