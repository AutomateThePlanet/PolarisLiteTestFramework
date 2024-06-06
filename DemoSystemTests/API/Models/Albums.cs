using Newtonsoft.Json;

namespace DemoSystemTests;
public class Albums : IEquatable<Albums>
{
    public Albums() => Tracks = new HashSet<Tracks>();

    [JsonProperty(Required = Required.Always)]
    public long AlbumId { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public string Title { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public long ArtistId { get; set; }

    [JsonProperty(Required = Required.AllowNull)]
    public Artists Artist { get; set; }
    [JsonProperty(Required = Required.AllowNull)]
    public ICollection<Tracks> Tracks { get; set; }

    public bool Equals(Albums other)
    {
        return AlbumId.Equals(other.AlbumId);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Albums);
    }
}
