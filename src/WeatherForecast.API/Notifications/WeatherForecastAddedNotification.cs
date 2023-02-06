
using MediatR;

namespace WeatherForecast.API.Notifications;

public record WeatherForecastAddedNotification(string Id) : INotification
{}