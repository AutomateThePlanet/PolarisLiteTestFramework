using PolarisLite.API;

namespace DemoSystemTests.Builder;
public class AlbumRepository : HttpRepository<Album>
{
    public AlbumRepository(string baseUrl)
        : base(baseUrl, "albums")
    {
    }
}
