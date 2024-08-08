namespace DemoSystemTests.Builder;
public class MediaType
{
    public MediaType() => Tracks = new HashSet<Track>();

    public long MediaTypeId { get; set; }
    public string Name { get; set; }

    public ICollection<Track> Tracks { get; set; }
}
