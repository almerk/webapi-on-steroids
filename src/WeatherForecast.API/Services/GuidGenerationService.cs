using MassTransit;

namespace WeatherForecast.API.Services
{
    public class GuidGenerationService
    {
        public virtual Guid New() => NewId.NextGuid();
    }
}