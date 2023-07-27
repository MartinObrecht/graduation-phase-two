namespace TechBlog.NewsManager.API.Domain.Entities
{
    public class BlogNew
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdateAt { get; set; }
    }
}
