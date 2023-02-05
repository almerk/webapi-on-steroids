using MediatR;

namespace WeatherForecast.API.Commands;

public record AddWeatherForecastCommand
    : IRequest<string>
{}
