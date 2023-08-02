using FluentValidation;
using TechBlog.NewsManager.API.Application.Mapper;
using TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create;

namespace TechBlog.NewsManager.API.DependencyInjection.Configurations
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BlogUserMapper));

            services.AddValidatorsFromAssemblyContaining<CreateBlogUserValidator>();

            return services;
        }
    }
}
