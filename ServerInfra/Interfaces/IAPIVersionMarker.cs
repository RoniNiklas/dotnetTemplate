namespace ServerInfra.Interfaces;

public interface IAPIVersionMarker
{
    public bool Deprecated { get; }
    public int VersionNumber { get; }
}
