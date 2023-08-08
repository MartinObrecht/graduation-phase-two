using TechBlog.NewsManager.API.Domain.Repositories;

namespace TechBlog.NewsManager.API.Infrastructure.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogNewsRepository BlogNew { get; }

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task<bool> CommitTransactionAsync(CancellationToken cancellationToken);
    }
}