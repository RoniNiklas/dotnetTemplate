using OneOf;

namespace Shared.Requests;

public record GetWeatherForecasts : IRequest<OneOf<WeatherForecastViewModel[], ValidationError>>
{
}
