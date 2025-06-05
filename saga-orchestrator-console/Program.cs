using AwesomeWebServiceOne.Data;
using AwesomeWebServiceOne.Models;
using saga_orchestrator_console.Models.Saga;

namespace saga_orchestrator_console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var newBlog = new Blog()
            {
                BlogId = 1,
                Url = "https://example.com",
                BlogUserId = 1,
            };
            var newPost = new Post()
            {
                PostId = 1,
                Title = "My first post",
                Content = "This is the content of my first post.",
                BlogId = 1,
                Blog = newBlog
            };

            var db = new BlogDbContext();

            var orchestrator = new SagaOrchestrator();
            orchestrator.AddStep(new AddNewBlogSagaStep(db));
            orchestrator.AddStep(new AddNewBlogSagaStep(db));

            //await orchestrator.ExecuteSagaAsync(newBlog);
        }
    }
}
