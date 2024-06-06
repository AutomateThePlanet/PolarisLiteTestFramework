using System.Collections.Generic;

namespace DemoSystemTests;
public class Playlists
{
    public Playlists() => PlaylistTrack = new HashSet<PlaylistTrack>();

    public long PlaylistId { get; set; }
    public string Name { get; set; }

    public ICollection<PlaylistTrack> PlaylistTrack { get; set; }
}
