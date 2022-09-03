using Shared.DTOs;

namespace Shared.Requests;
public record GetSingleWeatherForecast : IRequest<WeatherForecastViewModel>
{
    public int Id { get; set; }
}
