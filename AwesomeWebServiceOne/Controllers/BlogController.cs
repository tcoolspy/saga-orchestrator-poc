using System.Reflection.Metadata;
using AwesomeWebServiceOne.Data;
using AwesomeWebServiceOne.Models;
using AwesomeWebServiceOne.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeWebServiceOne.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController(BlogDbContext context, ILogger<BlogController> logger) : ControllerBase
{
    [HttpGet]
    [Route("api/blog")]
    public async Task<IEnumerable<Blog?>> GetBlogPosts()
    {
        return await Task.FromResult(context.Blogs);
    }

    [HttpGet]
    [Route("api/blog/{id}")]
    public async Task<Blog?> GetBlogPostById(int id)
    {
        return await context.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
    }

    [HttpPost]
    [Route("api/blog")]
    public async Task<bool> AddBlog(AddBlogRequestDto requestDto)
    {
        var blog = new Blog() { BlogUserId = requestDto.BlogUserId, Url = requestDto.Url };
        context.Blogs.Add(blog);
        await context.SaveChangesAsync();
        return true;
    }

    [HttpDelete]
    [Route("api/blog")]
    public async Task<bool> DeleteBlog(int id)
    {
        var blog = await context.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
        if (blog == null)
        {
            return false;
        }

        context.Blogs.Remove(blog);
        await context.SaveChangesAsync();
        return true;
    }
}