using Shared.DTOs;

namespace Shared.Requests;
public record GetSingleWeatherForecastQuery : IRequest<WeatherForecastViewModel>
{
    public int Id { get; set; }
}
