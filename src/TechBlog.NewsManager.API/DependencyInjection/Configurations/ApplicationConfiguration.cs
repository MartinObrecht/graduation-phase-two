using FluentValidation;
using TechBlog.NewsManager.API.Application.Mapper;
using TechBlog.NewsManager.API.Application.Strategies;
using TechBlog.NewsManager.API.Application.Strategies.GetBlogNewStrategy;
using TechBlog.NewsManager.API.Application.UseCases.BlogNews.Create;
using TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create;
using TechBlog.NewsManager.API.Domain.Domain.Strategies;
using TechBlog.NewsManager.API.Domain.Domain.Strategies.GetBlogUser;

namespace TechBlog.NewsManager.API.DependencyInjection.Configurations
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BlogUserMapper));
            services.AddAutoMapper(typeof(BlogNewMapper));

            services.AddValidatorsFromAssemblyContaining<CreateBlogUserValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateBlogNewValidator>();

            services.AddStrategies();

            return services;
        }

        private static IServiceCollection AddStrategies(this IServiceCollection services)
        {
            services.AddScoped<IStrategyHub, StrategyHub>();

            services.AddScoped<IGetBlogUserStrategy, GetByCreateDateStrategy>();
            services.AddScoped<IGetBlogUserStrategy, GetByCreateOrUpdateDateStrategy>();
            services.AddScoped<IGetBlogUserStrategy, GetByIdStrategy>();
            services.AddScoped<IGetBlogUserStrategy, GetByTagStrategy>();

            return services;
        }
    }
}
