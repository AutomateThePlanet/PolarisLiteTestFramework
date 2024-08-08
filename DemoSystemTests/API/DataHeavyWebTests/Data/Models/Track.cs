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
}
