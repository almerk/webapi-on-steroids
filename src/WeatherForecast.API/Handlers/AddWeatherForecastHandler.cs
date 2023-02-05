using MediatR;
using WeatherForecast.API.Commands;

namespace WeatherForecast.API.Handlers;

public class AddWeatherForecastHandler
    : IRequestHandler<AddWeatherForecastCommand, string>
{
    private readonly Services.WeatherForecastService _service;

    public AddWeatherForecastHandler(Services.WeatherForecastService service)
    {
        _service = service;
    }

    public async Task<string> Handle(AddWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        return await _service.AddAsync(cancellationToken);
    }
}
