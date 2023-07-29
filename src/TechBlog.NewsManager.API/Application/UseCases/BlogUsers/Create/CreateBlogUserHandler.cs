using TechBlog.NewsManager.API.Domain.Authentication;
using TechBlog.NewsManager.API.Domain.Logger;

namespace TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create
{
    public class CreateBlogUserHandler
    {
        public static string Route => "/api/v1/users";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        internal static async Task<IResult> Action(IIdentityManager identityManager,
                                                   ILoggerManager logger,
                                                   CreateBlogUser createBlogUser)
        {
            return Results.Ok();
        }
    }
}
