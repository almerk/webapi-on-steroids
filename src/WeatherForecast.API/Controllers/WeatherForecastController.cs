using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.API.Services;
using WeatherForecast.API.Queries;
using WeatherForecast.API.Commands;
using LanguageExt.Common;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GuidConversion;

namespace WeatherForecast.API.Controllers;

[ApiController]
[Route("wf")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastService _service;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public WeatherForecastController(WeatherForecastService service, IMediator mediator, IMapper mapper)
    {
        _service = service;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetWeatherForecasts")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllWeatherForecastsQuery(), cancellationToken);
        return Ok(_mapper.Map<IEnumerable<Contracts.WeatherForecastResponse>>(result));
    }

    [HttpGet("{id}", Name = "GetWeatherForecast")]
    public async Task<IActionResult> GetAsync(string id, CancellationToken cancellationToken)
    {
        if (!id.TryParseGuidFromUrlParameterString(out var parsed))
            return NotFound();

        var result = await _mediator.Send(new GetWeatherForecastByIdQuery(parsed));
        return MatchResult<Models.WeatherForecast, Contracts.WeatherForecastResponse>(result);
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
        if (!id.TryParseGuidFromUrlParameterString(out var parsed))
            return NotFound();

        var result = await _mediator.Send(new UpdateWeatherForecastCommand(parsed), cancellationToken);
        return MatchResult<Models.WeatherForecast, Contracts.WeatherForecastResponse>(result);
    }

    [HttpDelete("{id}", Name = "DeleteWeatherForecast")]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        if (!id.TryParseGuidFromUrlParameterString(out var parsed))
            return NotFound();
        
        await _mediator.Send(new DeleteWeatherForecastCommand(parsed), cancellationToken);
        return Ok();
    }

    private IActionResult MatchResult<TResult, TMappedResult>(Result<TResult> result)
    {
        return result.Match<IActionResult>(
            x => {
                if (typeof(TResult) == typeof(TMappedResult))  
                    return Ok(x);
                
                return Ok(_mapper.Map<TMappedResult>(x));
            },
            ex => ex switch
            {
                KeyNotFoundException => NotFound(),
                ValidationException v => BadRequest(v.Message),
                _ => StatusCode((int)System.Net.HttpStatusCode.InternalServerError, "Unknown internal error")
            }
        );
    }
}
