namespace AwesomeWebServiceOne.Models;

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    public int BlogUserId { get; set; }
    public List<Post> Posts { get; } = new();
}