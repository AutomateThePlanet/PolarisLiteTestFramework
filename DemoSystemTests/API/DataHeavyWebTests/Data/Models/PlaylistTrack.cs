namespace DemoSystemTests.Builder;
public class PlaylistTrack
{
    public long PlaylistId { get; set; }
    public long TrackId { get; set; }

    public Playlist Playlist { get; set; }
    public Track Track { get; set; }

    public override string ToString()
    {
        var playlistName = Playlist != null ? Playlist.Name : "Unknown Playlist";
        var trackTitle = Track != null ? Track.Name : "Unknown Track";
        return $"PlaylistId: {PlaylistId} ({playlistName}), TrackId: {TrackId} ({trackTitle})";
    }
}
