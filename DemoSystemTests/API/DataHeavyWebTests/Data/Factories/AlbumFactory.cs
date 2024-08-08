using Bogus;

namespace DemoSystemTests.Builder;
public static class AlbumFactory
{
    public static Album GenerateAlbum()
    {
        var faker = new Faker<Album>()
            .RuleFor(a => a.AlbumId, f => f.Random.Int(1))
            .RuleFor(a => a.Title, f => f.Lorem.Sentence())
            .RuleFor(a => a.ArtistId, f => f.Random.Int(1, 100));

        return faker.Generate();
    }
}
