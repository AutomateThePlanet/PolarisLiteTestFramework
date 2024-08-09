namespace DemoSystemTests.Builder;
public class Playlist
{
    public Playlist() => PlaylistTrack = new HashSet<PlaylistTrack>();

    public long PlaylistId { get; set; }
    public string Name { get; set; }

    public ICollection<PlaylistTrack> PlaylistTrack { get; set; }

    public override string ToString()
    {
        return $"PlaylistId: {PlaylistId}, Name: {Name}, Tracks Count: {PlaylistTrack.Count}";
    }
}
