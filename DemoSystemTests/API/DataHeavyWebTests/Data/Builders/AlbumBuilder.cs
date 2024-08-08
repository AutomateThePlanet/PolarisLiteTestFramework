using RepositoryDesignPatternTests;

namespace DemoSystemTests.Builder;
public class AlbumBuilder
{
    private Album _album;
    private AlbumRepository _albumRepository;

    public AlbumBuilder()
    {
        _album = new Album();
        _albumRepository = new AlbumRepository(Urls.BASE_API_URL);
    }

    public AlbumBuilder WithDefaultConfiguration()
    {
        _album = AlbumFactory.GenerateAlbum();
        return this;
    }

    public AlbumBuilder WithAlbumId(long albumId)
    {
        _album.AlbumId = albumId;
        return this;
    }

    public AlbumBuilder WithTitle(string title)
    {
        _album.Title = title;
        return this;
    }

    public AlbumBuilder WithArtistId(long artistId)
    {
        _album.ArtistId = artistId;
        return this;
    }

    public AlbumBuilder WithArtist(Artist artist)
    {
        _album.Artist = artist;
        return this;
    }

    public AlbumBuilder AddTrack(Track track)
    {
        _album.Tracks.Add(track);
        return this;
    }

    public AlbumBuilder WithTracks(ICollection<Track> tracks)
    {
        _album.Tracks = tracks;
        return this;
    }

    public Album Build()
    {
        _album = _albumRepository.CreateAsync(_album).Result;
        return _album;
    }
}

