using LanguageExt.Common;
using MediatR;

namespace WeatherForecast.API.Queries;

public record GetWeatherForecastByIdQuery(Guid Id) 
    : IRequest<Result<Models.WeatherForecast>>
{}
