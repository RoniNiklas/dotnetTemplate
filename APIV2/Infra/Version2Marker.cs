using ServerInfra.Interfaces;

namespace APIV2.Infra;

public class Version2Marker : IAPIVersionMarker
{
    public bool Deprecated => false;
    public int VersionNumber => 2;
}
