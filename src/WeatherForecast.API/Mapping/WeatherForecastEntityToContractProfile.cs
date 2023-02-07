using AutoMapper;
using GuidConversion;

namespace WeatherForecast.API.Mapping;

public class WeatherForecastEntityToContractProfile: Profile
{
    public WeatherForecastEntityToContractProfile()
    {
        CreateMap<Models.WeatherForecast, Contracts.WeatherForecastResponse>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToUrlParameterString()));
    }
}
