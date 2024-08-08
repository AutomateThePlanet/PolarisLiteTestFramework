using Bogus;

namespace DemoSystemTests.Builder;
public static class ArtistFactory
{
    public static Artist GenerateArtist()
    {
        var faker = new Faker<Artist>()
            .RuleFor(a => a.ArtistId, f => f.Random.Long(1))
            .RuleFor(a => a.Name, f => f.Person.FullName);

        return faker.Generate();
    }
}
