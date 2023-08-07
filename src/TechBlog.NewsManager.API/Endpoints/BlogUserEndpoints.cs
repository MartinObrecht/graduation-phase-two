using TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create;
using TechBlog.NewsManager.API.Application.ViewModels;
using TechBlog.NewsManager.API.Domain.Responses;

namespace TechBlog.NewsManager.API.Endpoints
{
    public static class BlogUserEndpoints
    {
        public static IApplicationBuilder MapBlogUsersEndpoints(this WebApplication app)
        {
            app.MapMethods(CreateBlogUserHandler.Route, CreateBlogUserHandler.Methods, CreateBlogUserHandler.Handle)
              .WithTags("Users")
              .WithDescription("Create a new Blog User")
              .WithDisplayName("Create Blog User")
              .ProducesValidationProblem()
              .Produces<BaseResponse>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest);

            return app;
        }
    }
}
