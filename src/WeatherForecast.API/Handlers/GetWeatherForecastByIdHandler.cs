using MediatR;
using WeatherForecast.API.Queries;

namespace WeatherForecast.API.Handlers;

public class GetWeatherForecastByIdHandler
    : IRequestHandler<GetWeatherForecastByIdQuery, Models.WeatherForecast>
{
    private readonly Services.WeatherForecastService _service;

    public GetWeatherForecastByIdHandler(Services.WeatherForecastService service)
    {
        _service = service;
    }

    public async Task<Models.WeatherForecast> Handle(GetWeatherForecastByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAsync(request.Id, cancellationToken);
    }
}
