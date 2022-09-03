using Shared.DTOs;

namespace Shared.Requests;

public record GetWeatherForecastsQuery : IRequest<WeatherForecastViewModel[]>
{
}
