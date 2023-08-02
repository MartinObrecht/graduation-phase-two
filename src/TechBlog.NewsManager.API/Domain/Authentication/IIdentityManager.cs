using TechBlog.NewsManager.API.Domain.Entities;

namespace TechBlog.NewsManager.API.Domain.Authentication
{
    public interface IIdentityManager
    {
        Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
        Task<bool> CreateUserAsync(BlogUser user, string password, CancellationToken cancellationToken);
    }
}
