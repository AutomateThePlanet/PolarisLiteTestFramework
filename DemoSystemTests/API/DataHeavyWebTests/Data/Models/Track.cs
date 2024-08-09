namespace DemoSystemTests.Builder;
public class Track
{
    public Track()
    {
        InvoiceItems = new HashSet<InvoiceItem>();
        PlaylistTrack = new HashSet<PlaylistTrack>();
    }

    public long TrackId { get; set; }
    public string Name { get; set; }
    public long? AlbumId { get; set; }
    public long MediaTypeId { get; set; }
    public long? GenreId { get; set; }
    public string Composer { get; set; }
    public long Milliseconds { get; set; }
    public long? Bytes { get; set; }
    public string UnitPrice { get; set; }

    public Album Album { get; set; }
    public Genre Genre { get; set; }
    public MediaType MediaType { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
    public ICollection<PlaylistTrack> PlaylistTrack { get; set; }

    public override string ToString()
    {
        var albumTitle = Album != null ? Album.Title : "Unknown Album";
        var genreName = Genre != null ? Genre.Name : "Unknown Genre";
        var mediaTypeName = MediaType != null ? MediaType.Name : "Unknown MediaType";
        return $"TrackId: {TrackId}, Name: {Name}, Album: {albumTitle}, Genre: {genreName}, " +
               $"MediaType: {mediaTypeName}, Composer: {Composer}, Duration: {Milliseconds} ms, " +
               $"Size: {Bytes ?? 0} bytes, Price: {UnitPrice}, InvoiceItems Count: {InvoiceItems.Count}, " +
               $"PlaylistTracks Count: {PlaylistTrack.Count}";
    }
}
