using ServerInfra.Interfaces;

namespace APIV1.Infra;

public class Version1Marker : IAPIVersionMarker
{
    public bool Deprecated => true;
    public int VersionNumber => 1;
}
