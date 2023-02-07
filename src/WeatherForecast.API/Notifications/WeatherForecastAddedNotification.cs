
using MediatR;

namespace WeatherForecast.API.Notifications;

public record WeatherForecastAddedNotification(Guid Id) : INotification
{}