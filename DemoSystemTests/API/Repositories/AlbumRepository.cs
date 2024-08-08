using PolarisLite.API;

namespace DemoSystemTests;
public class AlbumRepository : HttpRepository<Albums>
{
    public AlbumRepository(string baseUrl)
        : base(baseUrl, "albums")
    {
    }

    // Add methods specific to albums if needed
}
