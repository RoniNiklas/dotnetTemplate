namespace Shared.Requests;

public record GetWeatherForecasts : IRequest<WeatherForecastViewModel[]>
{
}
