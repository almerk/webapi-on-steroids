using LanguageExt.Common;
using MediatR;

namespace WeatherForecast.API.Commands;

public record UpdateWeatherForecastCommand(Guid Id)
    : IRequest<Result<Models.WeatherForecast>>
{}
