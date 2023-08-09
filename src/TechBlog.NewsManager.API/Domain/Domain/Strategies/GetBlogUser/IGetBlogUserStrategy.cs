namespace TechBlog.NewsManager.API.Domain.Domain.Strategies.GetBlogUser
{
    public interface IGetBlogUserStrategy
    {
        GetBlogUserStrategy Strategy { get; }
        Task<object> RunAsync(GetBlogUserStrategyBody body, CancellationToken cancellationToken);
    }
}
