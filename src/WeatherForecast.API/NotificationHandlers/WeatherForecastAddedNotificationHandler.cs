using MediatR;
using WeatherForecast.API.Notifications;

namespace WeatherForecast.API.NotificationHandlers;

public class WeatherForecastAddedNotificationHandler
    : INotificationHandler<Notifications.WeatherForecastAddedNotification>
{

    public Task Handle(WeatherForecastAddedNotification notification, CancellationToken cancellationToken)
    {
        System.Console.WriteLine($"Handled {nameof(WeatherForecastAddedNotification)}, Id={notification.Id}");
        return Task.CompletedTask;        
    }
}
