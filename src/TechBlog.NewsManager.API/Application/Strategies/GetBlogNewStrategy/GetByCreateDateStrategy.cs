﻿using TechBlog.NewsManager.API.Domain.Database;
using TechBlog.NewsManager.API.Domain.Exceptions;
using TechBlog.NewsManager.API.Domain.Logger;
using TechBlog.NewsManager.API.Domain.Strategies.GetBlogNews;

namespace TechBlog.NewsManager.API.Application.Strategies.GetBlogNewStrategy
{
    public class GetByCreateDateStrategy : IGetBlogNewsStrategy
    {
        private readonly ILoggerManager _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetBlogNewsStrategy Strategy => GetBlogNewsStrategy.GET_BY_CREATE_DATE;

        public GetByCreateDateStrategy(ILoggerManager logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<object> RunAsync(GetBlogNewsStrategyBody body, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Getting blognew by create date interval", ("strategy", Strategy), ("body", body));

            if (body is null || !body.ValidDateInterval)
            {
                _logger.LogInformation("Invalid body", ("strategy", Strategy), ("body", body));

                throw new BusinessException("Invalid strategy body");
            }

            var blogNews = await _unitOfWork.BlogNew.GetByCreatedDateAsync(body.From, body.To, cancellationToken);

            _logger.LogDebug("End getting blognew by create date interval", ("strategy", Strategy), ("body", body), ("newsFoundCount", blogNews.Count()));

            return blogNews;
        }
    }
}
