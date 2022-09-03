using OneOf;

namespace Handlers.WeatherForecast;
internal class GetSingleWeatherForecastQueryHandler : IRequestHandler<GetSingleWeatherForecast, OneOf<WeatherForecastViewModel, ValidationError>>
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

    public async Task<OneOf<WeatherForecastViewModel, ValidationError>> Handle(GetSingleWeatherForecast request, CancellationToken cancellationToken)
    {
        var valRes = request.Validate();

        return valRes.IsValid
            ? new WeatherForecastViewModel
                (
                    DateTime.Now.AddDays(request.Id),
                    Random.Shared.Next(-20, 55),
                    _summaries[Random.Shared.Next(_summaries.Length)]
                )
            : new ValidationError(valRes.Errors);
    }
}
