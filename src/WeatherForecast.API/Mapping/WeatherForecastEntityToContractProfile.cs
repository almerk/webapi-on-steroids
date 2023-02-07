using AutoMapper; 

namespace WeatherForecast.API.Mapping;

public class WeatherForecastEntityToContractProfile: Profile
{
    public WeatherForecastEntityToContractProfile()
    {
        CreateMap<Models.WeatherForecast, Contracts.WeatherForecastResponse>();
    }
}
