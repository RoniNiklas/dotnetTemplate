using OneOf;

namespace Shared.Requests;
public record GetSingleWeatherForecast(int Id) : IRequest<OneOf<WeatherForecastViewModel, ValidationError>>
{

}
