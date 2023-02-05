using MediatR;
using WeatherForecast.API.Queries;

namespace WeatherForecast.API.Handlers;

public class GetAllWeatherForecastsHandler
    : IRequestHandler<GetAllWeatherForecastsQuery, IEnumerable<Models.WeatherForecast>>
{
    private readonly Services.WeatherForecastService _service;

    public GetAllWeatherForecastsHandler(Services.WeatherForecastService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<Models.WeatherForecast>> Handle(GetAllWeatherForecastsQuery _, CancellationToken cancellationToken)
    {
        return await _service.GetAsync(cancellationToken);
    }
}
