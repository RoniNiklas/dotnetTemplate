using ServerInfra.Enums;
using ServerInfra.Models;
using Shared.DTOs;

namespace APIV2.Controllers;

public class WeatherController : ApplicationController
{
    public override int VersionNumber { get; init; }
    public override string ControllerPrefix => "weatherForecast";
    public override bool Deprecated { get; init; }

    string[] summaries = new[]
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

    public override List<ApiEndPointDefinition> Endpoints => new()
    {
        new(EndpointType.GET,
            () => Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastViewModel
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray()
        ),
        new(EndpointType.GET, (int forecastId) =>
            new WeatherForecastViewModel
            (
                DateTime.Now.AddDays(Random.Shared.Next(-20, 20)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            )
        , "{forecastId}")

    };

    public WeatherController(int versionNumber, bool deprecated)
    {
        VersionNumber = versionNumber;
        Deprecated = deprecated;
    }
}