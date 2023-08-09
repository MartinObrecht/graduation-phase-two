using TechBlog.NewsManager.API.Domain.Domain.Strategies.GetBlogUser;

namespace TechBlog.NewsManager.API.Domain.Domain.Strategies
{
    public interface IStrategyHub
    {
        Task<IGetBlogUserStrategy> FindGetBlogUserStrategy(GetBlogUserStrategy strategy, CancellationToken cancellationToken);
    }
}
