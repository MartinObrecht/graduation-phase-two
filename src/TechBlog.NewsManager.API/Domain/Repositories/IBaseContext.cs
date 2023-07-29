namespace TechBlog.NewsManager.API.Domain.Repositories
{
    public interface IBaseContext : IDisposable
    {
        Task<bool> AnyPendingMigrationsAsync();
        Task MigrateAsync();
        Task TestConnectionAsync();
    }
}
