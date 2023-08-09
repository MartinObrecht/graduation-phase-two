using TechBlog.NewsManager.API.Domain.Domain.Strategies.GetBlogUser;

namespace TechBlog.NewsManager.API.Application.Strategies.GetBlogNewStrategy
{
    public class GetByCreateDateStrategy : IGetBlogUserStrategy
    {
        public GetBlogUserStrategy Strategy => GetBlogUserStrategy.GET_BY_CREATE_DATE;

        public Task<object> RunAsync(GetBlogUserStrategyBody body, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
