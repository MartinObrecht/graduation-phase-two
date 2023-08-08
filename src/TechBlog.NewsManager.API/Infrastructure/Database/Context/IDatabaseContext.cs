using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TechBlog.NewsManager.API.Domain.Entities;
using TechBlog.NewsManager.API.Domain.Repositories;

namespace TechBlog.NewsManager.API.Infrastructure.Database.Context
{
    public interface IDatabaseContext : IBaseContext
    {
        public DbSet<BlogNew> BlogNew { get; }

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        Task<bool> CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
    }
}
