using MediatR;
using WeatherForecast.API.Commands;

namespace WeatherForecast.API.Handlers;

public class DeleteWeatherForecastHandler
    : IRequestHandler<DeleteWeatherForecastCommand>
{
    private readonly Services.WeatherForecastService _service;

    public DeleteWeatherForecastHandler(Services.WeatherForecastService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
