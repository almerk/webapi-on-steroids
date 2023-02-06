using LanguageExt.Common;
using MediatR;

namespace WeatherForecast.API.Queries;

public record GetWeatherForecastByIdQuery(string Id) 
    : IRequest<Result<Models.WeatherForecast>>
{}
