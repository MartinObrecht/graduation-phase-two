using TechBlog.NewsManager.API.Application.UseCases.BlogNews.Create;
using TechBlog.NewsManager.API.Domain.Authentication;

namespace TechBlog.NewsManager.API.Endpoints
{
    public static class BlogNewsEndpoints
    {
        public static IApplicationBuilder MapBlogNewsEndpoints(this WebApplication app)
        {
            app.MapMethods(CreateBlogNewHandler.Route, CreateBlogNewHandler.Methods, CreateBlogNewHandler.Handle)
              .WithTags("Blog New")
              .WithDescription("Create a new Blog New")
              .WithDisplayName("Create Blog New")
              .ProducesValidationProblem()
              .RequireAuthorization(AuthorizationPolicies.IsJournalist)
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest);

            return app;
        }
    }
}
