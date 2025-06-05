using AwesomeWebServiceOne.Data;
using AwesomeWebServiceOne.Models;
using SagaPattern.Core.Models;

namespace saga_orchestrator_console.Models.Saga;

public class AddNewBlogPostSagaStep(BlogDbContext blogDbContext) : ISagaStep
{
    public async Task<dynamic> ExecuteAsync(object entity)
    {
        if (entity is Post post)
        {
            var blog = await blogDbContext.Blogs.FindAsync(post.BlogId);
            if (blog != null)
            {
                blog.Posts.Add(post);
                await blogDbContext.SaveChangesAsync();
            }
            return true;
        }
        return false;
    }

    public async Task<dynamic> CompensateAsync(object entity)
    {
        if (entity is Post post)
        {
            var blog = await blogDbContext.Blogs.FindAsync(post.BlogId);
            if (blog != null)
            {
                var existingPost = blog.Posts.FirstOrDefault(p => p.PostId == post.PostId);
                if (existingPost != null)
                {
                    blog.Posts.Remove(existingPost);
                    await blogDbContext.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }
        return false;
    }
}