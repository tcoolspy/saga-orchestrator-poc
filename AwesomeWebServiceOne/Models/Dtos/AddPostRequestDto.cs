namespace AwesomeWebServiceOne.Models.Service;

public class AddPostRequestDto
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required int BlogId { get; set; }
}