namespace DemoSystemTests.Builder;
public class Artist
{
    public Artist() => Albums = new HashSet<Album>();

    public long ArtistId { get; set; }
    public string Name { get; set; }

    public ICollection<Album> Albums { get; set; }
}
