using LanguageExt.Common;
using MediatR;
using WeatherForecast.API.Commands;
using WeatherForecast.API.Queries;

namespace WeatherForecast.API.PipelineValidators;

public class WeatherForecastExistPipelineValidator
    : IPipelineBehavior<GetWeatherForecastByIdQuery, Result<Models.WeatherForecast>>,
    IPipelineBehavior<UpdateWeatherForecastCommand, Result<Models.WeatherForecast>>
{
    private readonly Services.WeatherForecastService _service;

    public WeatherForecastExistPipelineValidator(Services.WeatherForecastService service)
    {
        _service = service;
    }

    public async Task<Result<Models.WeatherForecast>> Handle(GetWeatherForecastByIdQuery request, RequestHandlerDelegate<Result<Models.WeatherForecast>> next, CancellationToken cancellationToken)
    {

        if (!await EntityExists(request.Id, cancellationToken))
            return new Result<Models.WeatherForecast>(new KeyNotFoundException());

        return await next();
    }

    public async Task<Result<Models.WeatherForecast>> Handle(UpdateWeatherForecastCommand request, RequestHandlerDelegate<Result<Models.WeatherForecast>> next, CancellationToken cancellationToken)
    {
        if (!await EntityExists(request.Id, cancellationToken))
            return new Result<Models.WeatherForecast>(new KeyNotFoundException());

        return await next();
    }

    private async ValueTask<bool> EntityExists(string id, CancellationToken cancellationToken)
    {
        return (await _service.FindAsync(id, cancellationToken)) is not null;
    }
}
