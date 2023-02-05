using MediatR;

namespace WeatherForecast.API.Queries;

public class GetAllWeatherForecastsQuery 
    : IRequest<IEnumerable<Models.WeatherForecast>>
{}
