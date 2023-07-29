using TechBlog.NewsManager.API.Domain.ValueObjects;

namespace TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create
{
    public class CreateBlogUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public BlogUserType BlogUserType { get; set; }
    }
}
