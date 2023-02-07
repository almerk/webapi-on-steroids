using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.API.Services;
using WeatherForecast.API.Queries;
using WeatherForecast.API.Commands;
using LanguageExt.Common;
using System.ComponentModel.DataAnnotations;

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
        var parsed = Guid.Parse(id);
        var result = await _mediator.Send(new GetWeatherForecastByIdQuery(parsed));
        return MatchResult(result);
    }

    [HttpPost(Name = "AddWeatherForecast")]
    public async Task<IActionResult> AddAsync(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AddWeatherForecastCommand());
        return Ok(result);
    }

    [HttpPut("{id}", Name = "UpdateWeatherForecastFull")]
    public async Task<IActionResult> UpdateAsync(string id, CancellationToken cancellationToken)
    {
        var parsed = Guid.Parse(id);
        var result = await _mediator.Send(new UpdateWeatherForecastCommand(parsed), cancellationToken);
        return MatchResult(result);
    }

    [HttpDelete("{id}", Name = "DeleteWeatherForecast")]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var parsed = Guid.Parse(id);
        await _mediator.Send(new DeleteWeatherForecastCommand(parsed), cancellationToken);
        return Ok();
    }

    private IActionResult MatchResult<TResult>(Result<TResult> result)
    {
        return result.Match<IActionResult>(
            x => Ok(x),
            ex => ex switch
            {
                KeyNotFoundException => NotFound(),
                ValidationException v => BadRequest(v.Message),
                _ => StatusCode((int)System.Net.HttpStatusCode.InternalServerError, "Unknown internal error")
            }
        );
    }
}
