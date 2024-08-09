using Newtonsoft.Json;

namespace DemoSystemTests.Builder;

public class Album
{
    public Album() => Tracks = new HashSet<Track>();

    public long AlbumId { get; set; }
    public string Title { get; set; }
    public long ArtistId { get; set; }
    public Artist Artist { get; set; }
    public ICollection<Track> Tracks { get; set; }

    public override string ToString()
    {
        var artistName = Artist != null ? Artist.Name : "Unknown Artist";
        return $"AlbumId: {AlbumId}, Title: {Title}, ArtistId: {ArtistId}, Artist: {artistName}";
    }
}
