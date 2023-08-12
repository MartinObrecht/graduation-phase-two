using TechBlog.NewsManager.API.Domain.Logger;
using TechBlog.NewsManager.API.Domain.Strategies;
using TechBlog.NewsManager.API.Domain.Strategies.GetBlogNews;

namespace TechBlog.NewsManager.API.Application.Strategies
{
    public class StrategyHub : IStrategyHub
    {
        private readonly IEnumerable<IGetBlogNewsStrategy> _getBlogNewsStrategies;
        private readonly ILoggerManager _logger;

        public StrategyHub(IEnumerable<IGetBlogNewsStrategy> getBlogNewsStrategies, ILoggerManager logger)
        {
            _getBlogNewsStrategies = getBlogNewsStrategies;
            _logger = logger;
        }

        public async Task<object> GetBlogNewsByStrategy(GetBlogNewsStrategy strategy, GetBlogNewsStrategyBody body, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Searching strategy", ("strategy", Enum.GetName(strategy)));

            var matchedStrategies = _getBlogNewsStrategies.Where(s => s.Strategy == strategy).ToList();

            if(matchedStrategies.Count == 0)
            {
                _logger.LogCritical("The strategy is not implemented", parameters: ("strategy", Enum.GetName(strategy)));

                throw new NotImplementedException("The strategy is not implemented");
            }

            if (matchedStrategies.Count > 1)
            {
                _logger.LogCritical("The strategy has more than one implementation", parameters: ("strategy", Enum.GetName(strategy)));

                throw new ArgumentException("The strategy has more than one implementation");
            }

            _logger.LogDebug("Strategy was found", ("strategy", Enum.GetName(strategy)));

            return await matchedStrategies[0].RunAsync(body, cancellationToken);
        }
    }
}
