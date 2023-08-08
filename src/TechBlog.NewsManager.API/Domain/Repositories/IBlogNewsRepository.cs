using TechBlog.NewsManager.API.Domain.Entities;

namespace TechBlog.NewsManager.API.Domain.Repositories
{
    public interface IBlogNewsRepository
    {
        Task AddAsync(BlogNew blogNews, CancellationToken cancellationToken = default);
    }
}
