namespace WeatherForecast.API.Contracts;

public record struct WeatherForecastResponse
{
    public string? Id { get; init; }
    public DateOnly Date { get; set; }
    public int TemperatureF { get; set; }
    public string? Summary { get; set; }
}
