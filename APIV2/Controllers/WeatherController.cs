using Microsoft.AspNetCore.Builder;
using ServerInfra.Interfaces;
using Shared.DTOs;

namespace APIV2.Controllers;

public class WeatherController : IApplicationController
{
    public override int VersionNumber { get; init; }
    public override string ControllerPrefix => "weatherForecast";
    public override bool Deprecated { get; init; }

    public WeatherController(int versionNumber, bool deprecated)
    {
        VersionNumber = versionNumber;
        Deprecated = deprecated;
    }

    public override void MapControllerMethods(WebApplication app)
    {
        var summaries = new[]
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

        AddGetWithDefaults(app, () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastViewModel
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        });

        AddGetWithDefaults(app, (int forecastId) =>
        {
            Console.WriteLine(forecastId);
            return
            new WeatherForecastViewModel
            (
                DateTime.Now.AddDays(Random.Shared.Next(-20, 20)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            );
        }, "{forecastId}");
    }
}