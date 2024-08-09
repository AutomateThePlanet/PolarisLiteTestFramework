using Bogus;

namespace DemoSystemTests.Builder;
public static class TrackFactory
{
    public static Track GenerateTrack()
    {
        var faker = new Faker<Track>()
            .RuleFor(t => t.TrackId, f => f.Random.Int(1))
            .RuleFor(t => t.Name, f => f.Lorem.Sentence())
            .RuleFor(t => t.AlbumId, f => f.Random.Int(1, 100))
            .RuleFor(t => t.UnitPrice, f => f.Finance.Amount().ToString())
            .RuleFor(t => t.Milliseconds, f => f.Random.Int(200000, 3600000));

        return faker.Generate();
    }
}
