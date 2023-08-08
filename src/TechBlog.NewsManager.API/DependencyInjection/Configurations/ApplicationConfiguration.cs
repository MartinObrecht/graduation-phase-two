using FluentValidation;
using TechBlog.NewsManager.API.Application.Mapper;
using TechBlog.NewsManager.API.Application.UseCases.BlogNews.Create;
using TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create;

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

            return services;
        }
    }
}
