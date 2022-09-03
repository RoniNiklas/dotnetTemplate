using OneOf;

namespace Handlers.WeatherForecast;
public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecasts, OneOf<WeatherForecastViewModel[], ValidationError>>
{
    private static readonly string[] _summaries = new[]
    {
                "Freezing",
                "Bracing",
                "Chilly",
                "Cool",
                "Mild",
                "Warm",
                "Balmy",
                "Hot",
                "Sweltering",
                "Scorching"
            };
    public async Task<OneOf<WeatherForecastViewModel[], ValidationError>> Handle(GetWeatherForecasts request, CancellationToken cancellationToken)
    {
        if (Random.Shared.NextSingle() > 0.5)
        {
            return new ValidationError(new Dictionary<string, string>()
                {
                    { "key", "value" }
                });
        }
        return Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastViewModel
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    _summaries[Random.Shared.Next(_summaries.Length)]
                ))
                .ToArray();
    }
}
