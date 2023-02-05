using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.API.Services;
using WeatherForecast.API.Queries;
//using WeatherForecast.API.Commands;

namespace WeatherForecast.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastService _service;
    private readonly IMediator _mediator;
    public WeatherForecastController(WeatherForecastService service, IMediator mediator)
    {
        _service = service;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecasts")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllWeatherForecastsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetWeatherForecast")]
    public async Task<IActionResult> GetAsync(string id, CancellationToken cancellationToken)
    {
       var result = await _service.GetAsync(id, cancellationToken);
       return Ok(result);
    }

    [HttpPost(Name = "AddWeatherForecast")]
    public async Task<IActionResult> AddAsync(CancellationToken cancellationToken)
    {
        var result = await _service.AddAsync(cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id}", Name = "UpdateWeatherForecastFull")]
    public async Task<IActionResult> UpdateAsync(string id, CancellationToken cancellationToken)
    {
       var result = await _service.UpdateAsync(id, cancellationToken);
       return Ok(result);
    }

    [HttpDelete("{id}", Name = "DeleteWeatherForecast")]
    public async Task<IActionResult> UdpateAsync(string id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return Ok();
    } 
}
