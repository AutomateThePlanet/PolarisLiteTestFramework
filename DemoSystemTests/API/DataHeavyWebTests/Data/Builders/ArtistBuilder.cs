using RepositoryDesignPatternTests;

namespace DemoSystemTests.Builder;
public class ArtistBuilder
{
    private Artist _artist;
    private ArtistRepository _artistRepository;

    public ArtistBuilder()
    {
        _artist = new Artist();
        _artistRepository = new ArtistRepository(Urls.BASE_API_URL);
    }

    public ArtistBuilder WithDefaultConfiguration()
    {
        _artist = ArtistFactory.GenerateArtist();
        return this;
    }

    public ArtistBuilder WithArtistId(long artistId)
    {
        _artist.ArtistId = artistId;
        return this;
    }

    public ArtistBuilder WithName(string name)
    {
        _artist.Name = name;
        return this;
    }

    public ArtistBuilder AddAlbum(Album album)
    {
        _artist.Albums.Add(album);
        return this;
    }

    public ArtistBuilder WithAlbums(ICollection<Album> albums)
    {
        _artist.Albums = albums;
        return this;
    }

    public Artist Build()
    {
        _artist = _artistRepository.CreateAsync(_artist).Result;
        return _artist;
    }

    public ArtistBuilder WithAlbums(IEnumerable<Album> albums)
    {
        throw new NotImplementedException();
    }
}

