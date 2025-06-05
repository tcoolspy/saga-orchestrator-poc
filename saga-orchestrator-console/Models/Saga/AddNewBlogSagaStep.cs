using AwesomeWebServiceOne.Data;
using AwesomeWebServiceOne.Models;
using SagaPattern.Core.Models;

namespace saga_orchestrator_console.Models.Saga;

public class AddNewBlogSagaStep(BlogDbContext blogDbContext) : ISagaStep
{
    public async Task<dynamic> ExecuteAsync(object entity)
    {
        if (entity is Blog blog)
        {
            var existingBlog = await blogDbContext.Blogs.FindAsync(blog.BlogId);
            if (existingBlog == null)
            {
                blogDbContext.Blogs.Add(blog);
                await blogDbContext.SaveChangesAsync();
            }
            return true;
        }
        return false;
    }

    public async Task<dynamic> CompensateAsync(object entity)
    {
        if (entity is Blog blog)
        {
            var existingBlog = await blogDbContext.Blogs.FindAsync(blog.BlogId);
            if (existingBlog != null)
            {
                blogDbContext.Blogs.Remove(existingBlog);
                await blogDbContext.SaveChangesAsync();
            }
            return true;
        }
        return false;
    }
}