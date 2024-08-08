using PolarisLite.API;

namespace DemoSystemTests.Builder;
public class TrackRepository : HttpRepository<Track>
{
    public TrackRepository(string baseUrl)
        : base(baseUrl, "tracks" )
    {
    }

    // Add methods specific to tracks if needed
}
