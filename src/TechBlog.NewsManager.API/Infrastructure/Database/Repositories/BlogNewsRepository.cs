using TechBlog.NewsManager.API.Domain.Entities;
using TechBlog.NewsManager.API.Domain.Repositories;
using TechBlog.NewsManager.API.Infrastructure.Database.Context;

namespace TechBlog.NewsManager.API.Infrastructure.Database.Repositories
{
    public sealed class BlogNewsRepository : IBlogNewsRepository
    {
        private readonly IDatabaseContext _context;

        public BlogNewsRepository(IDatabaseContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(BlogNew blogNew, CancellationToken cancellationToken = default)
        {
            await _context.BlogNew.AddAsync(blogNew, cancellationToken);
        }
    }
}
