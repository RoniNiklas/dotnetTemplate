namespace Handlers.WeatherForecast;
public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecasts, RequestResult<WeatherForecastViewModel[]>>
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
    public Task<RequestResult<WeatherForecastViewModel[]>> Handle(GetWeatherForecasts request, CancellationToken cancellationToken)
    {
        if (Random.Shared.NextSingle() > 0.5)
        {
            return Task.FromResult(
                RequestResult.Invalid<WeatherForecastViewModel[]>(new Dictionary<string, string>()
                {
                    { "key", "value" }
                });
        }
        return Task.FromResult(
            RequestResult.Success(Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastViewModel
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    _summaries[Random.Shared.Next(_summaries.Length)]
                ))
                .ToArray()));
    }
}
