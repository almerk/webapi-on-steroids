using MediatR;

namespace WeatherForecast.API.Commands;

public record DeleteWeatherForecastCommand(Guid Id) : IRequest
{}
