using MediatR;
using WeatherForecast.API.Commands;

namespace WeatherForecast.API.Handlers;

public class AddWeatherForecastHandler
    : IRequestHandler<AddWeatherForecastCommand, string>
{
    private readonly Services.WeatherForecastService _service;
    private readonly IMediator _mediator;

    public AddWeatherForecastHandler(Services.WeatherForecastService service, IMediator mediator)
    {
        _service = service;
        _mediator = mediator;
    }

    public async Task<string> Handle(AddWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.AddAsync(cancellationToken);
        await _mediator.Publish(new Notifications.WeatherForecastAddedNotification(result));
        return result.ToString();
    }
}
