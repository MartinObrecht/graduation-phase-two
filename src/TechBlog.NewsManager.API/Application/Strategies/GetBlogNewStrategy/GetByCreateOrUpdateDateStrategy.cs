using TechBlog.NewsManager.API.Domain.Domain.Strategies.GetBlogUser;

namespace TechBlog.NewsManager.API.Application.Strategies.GetBlogNewStrategy
{
    public class GetByCreateOrUpdateDateStrategy : IGetBlogUserStrategy
    {
        public GetBlogUserStrategy Strategy => GetBlogUserStrategy.GET_BY_CREATE_OR_UPDATE_DATE;

        public Task<object> RunAsync(GetBlogUserStrategyBody body, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
