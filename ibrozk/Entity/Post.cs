namespace ibrozk.Entity;

public class Post
{
    public int PostId { get; set; }
    public string? Urlname { get; set; }
    public int? Column { get; set; }
    public List<Tag> Tags { get; set; } = new List<Tag>();
}
