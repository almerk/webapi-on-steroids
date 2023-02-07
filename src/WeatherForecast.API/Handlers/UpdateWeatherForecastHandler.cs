
using LanguageExt.Common;
using MediatR;
using WeatherForecast.API.Commands;

namespace WeatherForecast.API.Handlers;

public class UpdateWeatherForecastHandler
    : IRequestHandler<UpdateWeatherForecastCommand, Result<Models.WeatherForecast>>
{
    private readonly Services.WeatherForecastService _service;

    public UpdateWeatherForecastHandler(Services.WeatherForecastService service)
    {
        _service = service;
    }

    public async Task<Result<Models.WeatherForecast>> Handle(UpdateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(request.Id, cancellationToken);
    }
}
