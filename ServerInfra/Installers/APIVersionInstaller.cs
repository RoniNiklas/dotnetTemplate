using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServerInfra.Interfaces;

namespace ServerInfra.Installers;

public static class APIVersionInstaller
{
    public static void InstallVersionsFromAssemblyWithMarkers(this IServiceCollection services, params IAPIVersionMarker[] markers)
    {

        services.AddApiVersioning(options =>
        {
            /*
            var versions = markers.OrderByDescending(m => m.VersionNumber);
            var defaultVersion = versions.First().VersionNumber;
            options.DefaultApiVersion = new ApiVersion(defaultVersion, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            */
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void InstallControllersFromAssemblyWithMarkers(this WebApplication app, params IAPIVersionMarker[] markers)
    {
        foreach (var marker in markers)
        {

            var assembly = marker.GetType().Assembly;
            var types = assembly.DefinedTypes.Where(item => typeof(IApplicationController).IsAssignableFrom(item) && !item.IsInterface && !item.IsAbstract);
            var controllers = types.Select(type => Activator.CreateInstance(type, marker.VersionNumber, marker.Deprecated)).Cast<IApplicationController>();

            foreach (var controller in controllers)
            {
                controller.MapControllerMethods(app);
            }

        }
    }
}
