using MediatR;

namespace WeatherForecast.API.Queries;

public record GetWeatherForecastByIdQuery(string Id) 
    : IRequest<Models.WeatherForecast>
{}
