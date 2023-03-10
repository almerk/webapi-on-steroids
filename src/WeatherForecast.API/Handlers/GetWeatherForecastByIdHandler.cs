using LanguageExt.Common;
using MediatR;
using WeatherForecast.API.Queries;

namespace WeatherForecast.API.Handlers;

public class GetWeatherForecastByIdHandler
    : IRequestHandler<GetWeatherForecastByIdQuery, Result<Models.WeatherForecast>>
{
    private readonly Services.WeatherForecastService _service;

    public GetWeatherForecastByIdHandler(Services.WeatherForecastService service)
    {
        _service = service;
    }

    public async Task<Result<Models.WeatherForecast>> Handle(GetWeatherForecastByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.FindAsync(request.Id, cancellationToken);
       
       if (result is null)
            throw new InvalidOperationException($"Cannot find {request.Id}");

        return result;
    }
}
