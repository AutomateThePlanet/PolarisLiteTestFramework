namespace DemoSystemTests.Builder;

using PolarisLite.API;

public class ArtistRepository : HttpRepository<Artist>
{
    public ArtistRepository(string baseUrl)
        : base(baseUrl, "artists")
    {
    }
}

