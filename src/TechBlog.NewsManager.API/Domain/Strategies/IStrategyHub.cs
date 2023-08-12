using TechBlog.NewsManager.API.Domain.Strategies.GetBlogNews;

namespace TechBlog.NewsManager.API.Domain.Strategies
{
    public interface IStrategyHub
    {
        Task<object> GetBlogNewsByStrategy(GetBlogNewsStrategy strategy, GetBlogNewsStrategyBody body, CancellationToken cancellationToken);
    }
}
