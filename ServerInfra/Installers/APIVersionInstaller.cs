using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServerInfra.Interfaces;
using ServerInfra.Models;

namespace ServerInfra.Installers;

public static class APIVersionInstaller
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void InstallControllersFromAssemblyWithMarkers(this WebApplication app, params IAPIVersionMarker[] markers)
    {
        foreach (var marker in markers)
        {
            var assembly = marker.GetType().Assembly;
            var types = assembly.DefinedTypes.Where(item => typeof(ApplicationController).IsAssignableFrom(item) && !item.IsInterface && !item.IsAbstract);
            var controllers = types.Select(type => Activator.CreateInstance(type, marker.VersionNumber, marker.Deprecated)).Cast<ApplicationController>();

            foreach (var controller in controllers)
            {
                controller.MapControllerMethods(app);
            }
        }
    }
}
