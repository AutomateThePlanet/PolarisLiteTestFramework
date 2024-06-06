using DemoSystemTests;

namespace RepositoryDesignPatternTests.Data.Repositories;
public class TrackRepository : HttpRepository<Tracks>
{
    public TrackRepository(string baseUrl)
        : base(baseUrl, "tracks" )
    {
    }

    // Add methods specific to tracks if needed
}
