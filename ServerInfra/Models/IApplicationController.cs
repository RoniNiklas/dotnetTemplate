using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ServerInfra.Enums;

namespace ServerInfra.Models;

public abstract class ApplicationController
{
    public abstract int VersionNumber { get; init; }
    public abstract bool Deprecated { get; init; }
    public abstract string ControllerPrefix { get; }
    public abstract List<ApiEndPointDefinition> Endpoints { get; }

    public void MapControllerMethods(WebApplication app)
    {
        foreach (var endpoint in Endpoints)
        {
            switch (endpoint.Type)
            {
                case EndpointType.GET:
                    {
                        AddGetWithDefaults(app, endpoint.Handler, endpoint.CustomPath);
                        break;
                    }
                case EndpointType.POST:
                    {
                        AddPostWithDefaults(app, endpoint.Handler, endpoint.CustomPath);
                        break;
                    }
                case EndpointType.PUT:
                    {
                        AddPutWithDefaults(app, endpoint.Handler, endpoint.CustomPath);
                        break;
                    }
                case EndpointType.DELETE:
                    {
                        AddDeleteWithDefaults(app, endpoint.Handler, endpoint.CustomPath);
                        break;
                    }
                default: throw new Exception("Unhandled endpoint type");
            }
        }
    }

    public void AddGetWithDefaults(WebApplication app, Delegate handler, string? customPath = null)
    {
        app.MapGet($"v{VersionNumber}/{ControllerPrefix}{(customPath != null ? $"/{customPath}" : "")}", handler)
            .WithTags($"v{VersionNumber}/{ControllerPrefix}"); ;
    }

    public void AddPostWithDefaults(WebApplication app, Delegate handler, string? customPath = null)
    {
        app.MapPost($"v{VersionNumber}/{ControllerPrefix}{(customPath != null ? $"/{customPath}" : "")}", handler)
            .WithTags($"v{VersionNumber}/{ControllerPrefix}"); ;
    }

    public void AddPutWithDefaults(WebApplication app, Delegate handler, string? customPath = null)
    {
        app.MapPut($"v{VersionNumber}/{ControllerPrefix}{(customPath != null ? $"/{customPath}" : "")}", handler)
            .WithTags($"v{VersionNumber}/{ControllerPrefix}"); ;
    }

    public void AddDeleteWithDefaults(WebApplication app, Delegate handler, string? customPath = null)
    {
        app.MapDelete($"v{VersionNumber}/{ControllerPrefix}{(customPath != null ? $"/{customPath}" : "")}", handler)
            .WithTags($"v{VersionNumber}/{ControllerPrefix}"); ;
    }
}
