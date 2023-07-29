using TechBlog.NewsManager.API.Application.Contracts.Responses;
using TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create;
using TechBlog.NewsManager.API.Application.ViewModels;

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
              .Produces<BaseResponseWithValue<AccessTokenViewModel>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest);

            return app;
        }
    }
}
