using ServerInfra.Enums;

namespace ServerInfra.Models;
public record ApiEndPointDefinition(EndpointType Type, Delegate Handler, string? CustomPath = null);
