using LanguageExt.Common;
using MediatR;

namespace WeatherForecast.API.Commands;

public record UpdateWeatherForecastCommand(string Id)
    : IRequest<Result<Models.WeatherForecast>>
{}
