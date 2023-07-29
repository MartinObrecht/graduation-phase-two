using TechBlog.NewsManager.API.Domain.ValueObjects;

namespace TechBlog.NewsManager.API.Domain.Entities
{
    public sealed class BlogUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string Name { get; set; }
        public BlogUserType BlogUserType { get; set; }
        public ICollection<BlogNew> WrittenNews { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdateAt { get; set; }
    }
}
