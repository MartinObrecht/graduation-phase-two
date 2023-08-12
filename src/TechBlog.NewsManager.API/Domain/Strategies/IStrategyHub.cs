using TechBlog.NewsManager.API.Domain.Strategies.GetBlogNews;

namespace TechBlog.NewsManager.API.Domain.Strategies
{
    public interface IStrategyHub
    {
        Task<IGetBlogNewsStrategy> FindGetBlogNewsStrategy(GetBlogNewsStrategy strategy, CancellationToken cancellationToken);
    }
}
