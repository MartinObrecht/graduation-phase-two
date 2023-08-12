using TechBlog.NewsManager.API.Domain.Database;
using TechBlog.NewsManager.API.Domain.Exceptions;
using TechBlog.NewsManager.API.Domain.Logger;
using TechBlog.NewsManager.API.Domain.Strategies.GetBlogNews;

namespace TechBlog.NewsManager.API.Application.Strategies.GetBlogNewStrategy
{
    public class GetByIdStrategy : IGetBlogNewsStrategy
    {
        private readonly ILoggerManager _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetBlogNewsStrategy Strategy => GetBlogNewsStrategy.GET_BY_ID;

        public GetByIdStrategy(ILoggerManager logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<object> RunAsync(GetBlogNewsStrategyBody body, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting blognew by id", ("strategy", Strategy), ("body", body));

            if (body is null || !body.ValidId)
            {
                _logger.LogInformation("Invalid body", ("strategy", Strategy), ("body", body));

                throw new BusinessException("Invalid strategy body");
            }

            var blogNew = await _unitOfWork.BlogNew.GetByIdAsync(body.Id, cancellationToken);

            if (!blogNew.Enabled)
            {
                _logger.LogInformation("Blog new don't exists", ("strategy", Strategy), ("body", body), ("blogNew", blogNew));

                throw new BusinessException("Blog new doesn't exists");
            }

            blogNew.Author.WrittenNews.Clear();

            _logger.LogDebug("Blog new found", ("strategy", Strategy), ("body", body), ("blogNew", blogNew));

            return blogNew;
        }
    }
}
