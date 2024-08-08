using Newtonsoft.Json;

namespace DemoSystemTests.Builder;

public class Album : IEquatable<Album>
{
    public Album() => Tracks = new HashSet<Track>();

    [JsonProperty(Required = Required.Always)]
    public long AlbumId { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public string Title { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public long ArtistId { get; set; }

    [JsonProperty(Required = Required.AllowNull)]
    public Artist Artist { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public ICollection<Track> Tracks { get; set; }

    public bool Equals(Album other)
    {
        return AlbumId.Equals(other.AlbumId);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Album);
    }
}
