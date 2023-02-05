using MediatR;

namespace WeatherForecast.API.Commands;

public record DeleteWeatherForecastCommand(string Id) : IRequest
{}
