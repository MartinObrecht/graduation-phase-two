using TechBlog.NewsManager.API.Domain.Domain.Strategies;
using TechBlog.NewsManager.API.Domain.Domain.Strategies.GetBlogUser;
using TechBlog.NewsManager.API.Domain.Logger;

namespace TechBlog.NewsManager.API.Application.Strategies
{
    public class StrategyHub : IStrategyHub
    {
        private readonly IEnumerable<IGetBlogUserStrategy> _getBlogUserStrategies;
        private readonly ILoggerManager _logger;

        public StrategyHub(IEnumerable<IGetBlogUserStrategy> getBlogUserStrategies, ILoggerManager logger)
        {
            _getBlogUserStrategies = getBlogUserStrategies;
            _logger = logger;
        }

        public Task<IGetBlogUserStrategy> FindGetBlogUserStrategy(GetBlogUserStrategy strategy, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Searching strategy", ("strategy", Enum.GetName(strategy)));

            var matchedStrategies = _getBlogUserStrategies.Where(s => s.Strategy == strategy).ToList();

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

            return Task.FromResult(matchedStrategies[0]);
        }
    }
}
