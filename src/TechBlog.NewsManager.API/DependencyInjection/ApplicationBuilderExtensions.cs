using TechBlog.NewsManager.API.DependencyInjection.Configurations;

namespace TechBlog.NewsManager.API.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder UseDependencyInjection(this WebApplication app, bool isDevelopment)
        {
            app.UseApiConfiguration(isDevelopment);

            app.UseInfrastructureConfiguration();

            app.UseSwaggerConfiguration();

            return app;
        }
    }
}
