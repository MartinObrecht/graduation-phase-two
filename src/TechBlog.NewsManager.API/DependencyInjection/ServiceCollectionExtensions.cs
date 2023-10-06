using TechBlog.NewsManager.API.DependencyInjection.Configurations;

namespace TechBlog.NewsManager.API.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            services.AddApiConfiguration();

            services.AddApplicationConfiguration();

            services.AddInfrastructureConfiguration(configuration, isDevelopment);

            services.AddSwaggerConfiguration();

            return services;
        }
    }
}
