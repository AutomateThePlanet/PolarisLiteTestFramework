namespace DemoSystemTests.Builder;
public class Genre
{
    public Genre() => Tracks = new HashSet<Track>();

    public long GenreId { get; set; }
    public string Name { get; set; }

    public ICollection<Track> Tracks { get; set; }

    public override string ToString()
    {
        return $"GenreId: {GenreId}, Name: {Name}, Tracks Count: {Tracks.Count}";
    }
}
