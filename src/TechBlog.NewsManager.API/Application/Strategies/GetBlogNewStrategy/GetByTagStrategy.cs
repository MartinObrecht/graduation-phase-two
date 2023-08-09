using TechBlog.NewsManager.API.Domain.Domain.Strategies.GetBlogUser;

namespace TechBlog.NewsManager.API.Application.Strategies.GetBlogNewStrategy
{
    public class GetByTagStrategy : IGetBlogUserStrategy
    {
        public GetBlogUserStrategy Strategy => GetBlogUserStrategy.GET_BY_TAGS;

        public Task<object> RunAsync(GetBlogUserStrategyBody body, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
