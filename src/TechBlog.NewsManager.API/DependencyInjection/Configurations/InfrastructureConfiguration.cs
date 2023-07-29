using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TechBlog.NewsManager.API.Domain.Authentication;
using TechBlog.NewsManager.API.Domain.Entities;
using TechBlog.NewsManager.API.Domain.Logger;
using TechBlog.NewsManager.API.Domain.Repositories;
using TechBlog.NewsManager.API.Infrastructure.Authentication.Configuration.Context;
using TechBlog.NewsManager.API.Infrastructure.Database.Context;
using TechBlog.NewsManager.API.Infrastructure.Database.Repositories;
using TechBlog.NewsManager.API.Infrastructure.Identity;
using TechBlog.NewsManager.API.Infrastructure.Logger;

namespace TechBlog.NewsManager.API.DependencyInjection.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILoggerManager, ConsoleLogger>();

            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();

            services.AddScoped<IDatabaseContext, SqlServerContext>();
            services.AddDbContext<IDatabaseContext, SqlServerContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
            });

            services.AddScoped<IIdentityManager, IdentityManager>();
            services.AddScoped<IIdentityContext, IdentityContext>();
            services.AddDbContext<IIdentityContext, IdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
            });
            services.AddIdentity<BlogUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            }
            ).AddEntityFrameworkStores<IdentityContext>()
             .AddDefaultTokenProviders();

            return services;
        }

        public static IApplicationBuilder UseInfrastructureConfiguration(this WebApplication app)
        {
            using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();

            using var databaseContext = serviceScope.ServiceProvider.GetRequiredService<IDatabaseContext>();
            using var identityContext = serviceScope.ServiceProvider.GetRequiredService<IIdentityContext>();

            if (databaseContext.AnyPendingMigrationsAsync().Result)
                databaseContext.MigrateAsync().Wait();

            if (identityContext.AnyPendingMigrationsAsync().Result)
                identityContext.MigrateAsync().Wait();

            return app;
        }
    }
}
