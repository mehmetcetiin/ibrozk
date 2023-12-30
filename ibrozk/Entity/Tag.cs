namespace ibrozk.Entity;

public class Tag
{
    public int TagId { get; set; }
    public string? Tagname { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();
}
