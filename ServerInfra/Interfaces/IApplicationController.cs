using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ServerInfra.Interfaces;

public abstract class IApplicationController
{
    public abstract int VersionNumber { get; init; }
    public abstract bool Deprecated { get; init; }
    public abstract string ControllerPrefix { get; }
    public abstract void MapControllerMethods(WebApplication app);

    public void AddGetWithDefaults(WebApplication app, Delegate handler, string? customPath = null)
    {
        app.MapGet($"v{VersionNumber}/{ControllerPrefix}{(customPath != null ? $"/{customPath}" : "")}", handler)
            .WithTags($"v{VersionNumber}/{ControllerPrefix}"); ;
    }
}
