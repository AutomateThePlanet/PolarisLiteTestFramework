using RepositoryDesignPatternTests;

namespace DemoSystemTests.Builder;
public class TrackBuilder
{
    private Track _track;
    private TrackRepository _trackRepository;
    private AlbumRepository _albumRepository;

    public TrackBuilder()
    {
        _track = new Track();
        _trackRepository = new TrackRepository(Urls.BASE_API_URL);
        _albumRepository = new AlbumRepository(Urls.BASE_API_URL);
    }

    public TrackBuilder WithDefaultConfiguration()
    {
        _track = TrackFactory.GenerateTrack();
        return this;
    }

    public TrackBuilder WithTrackId(long trackId)
    {
        _track.TrackId = trackId;
        return this;
    }

    public TrackBuilder WithName(string name)
    {
        _track.Name = name;
        return this;
    }

    public TrackBuilder WithAlbumId(long? albumId)
    {
        _track.AlbumId = albumId;
        return this;
    }

    public TrackBuilder WithMediaTypeId(long mediaTypeId)
    {
        _track.MediaTypeId = mediaTypeId;
        return this;
    }

    public TrackBuilder WithGenreId(long? genreId)
    {
        _track.GenreId = genreId;
        return this;
    }

    public TrackBuilder WithComposer(string composer)
    {
        _track.Composer = composer;
        return this;
    }

    public TrackBuilder WithDuration(int milliseconds)
    {
        _track.Milliseconds = milliseconds;
        return this;
    }

    public TrackBuilder WithBytes(long? bytes)
    {
        _track.Bytes = bytes;
        return this;
    }

    public TrackBuilder WithUnitPrice(string unitPrice)
    {
        _track.UnitPrice = unitPrice;
        return this;
    }

    public TrackBuilder WithDefaultAlbum()
    {
        var album = AlbumFactory.GenerateAlbum();
        return WithAlbum(album);
    }

    public TrackBuilder WithAlbum(Album album)
    {
        album = _albumRepository.CreateAsync(album).Result;
        _track.Album = album;
        return this;
    }

    public TrackBuilder WithGenre(Genre genre)
    {
        _track.Genre = genre;
        return this;
    }

    public TrackBuilder WithMediaType(MediaType mediaType)
    {
        _track.MediaType = mediaType;
        return this;
    }

    public TrackBuilder WithInvoiceItem(InvoiceItem invoiceItem)
    {
        _track.InvoiceItems.Add(invoiceItem);
        return this;
    }

    public TrackBuilder WithPlaylistTrack(PlaylistTrack playlistTrack)
    {
        _track.PlaylistTrack.Add(playlistTrack);
        return this;
    }

    public TrackBuilder WithInvoiceItems(ICollection<InvoiceItem> invoiceItems)
    {
        _track.InvoiceItems = invoiceItems;
        return this;
    }

    public TrackBuilder WithPlaylistTracks(ICollection<PlaylistTrack> playlistTracks)
    {
        _track.PlaylistTrack = playlistTracks;
        return this;
    }

    public Track Build()
    {
        _track = _trackRepository.CreateAsync(_track).Result;
        return _track;
    }
}
