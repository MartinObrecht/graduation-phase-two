using TechBlog.NewsManager.API.Domain.ValueObjects;

namespace TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create
{
    public sealed record CreateBlogUserRequest(string Name, string Email, string Password, BlogUserType BlogUserType);
}
