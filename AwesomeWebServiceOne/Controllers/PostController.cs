using AwesomeWebServiceOne.Data;
using AwesomeWebServiceOne.Models;
using AwesomeWebServiceOne.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeWebServiceOne.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController(BlogDbContext context, ILogger<BlogController> logger) : ControllerBase
{
    [HttpGet]
    [Route("api/post")]
    public async Task<IEnumerable<Post?>> GetAllPosts()
    {
        return await Task.FromResult(context.Posts);
    }

    [HttpGet]
    [Route("api/post/{id}")]
    public async Task<Post?> GetPostById(int id)
    {
        return await context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
    }

    [HttpPost]
    [Route("api/post")]
    public async Task<bool> AddPost(AddPostRequestDto requestDto)
    {
        var post = new Post { Title = requestDto.Title, Content = requestDto.Content, BlogId = requestDto.BlogId };

        context.Posts.Add(post);
        await context.SaveChangesAsync();
        return true;
    }

    [HttpDelete]
    [Route("api/post")]
    public async Task<bool> DeletePost(int id)
    {
        var post = await context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
        if (post == null)
        {
            return false;
        }

        context.Posts.Remove(post);
        await context.SaveChangesAsync();
        return true;
    }
}