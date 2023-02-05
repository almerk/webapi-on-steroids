using MediatR;

namespace WeatherForecast.API.Commands;

public record UpdateWeatherForecastCommand(string Id)
    : IRequest<Models.WeatherForecast>
{}
