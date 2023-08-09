namespace TechBlog.NewsManager.API.Domain.Domain.Strategies.GetBlogUser
{
    public class GetBlogUserStrategyBody
    {
        public Guid Id { get; set; }
        public string[] Tags { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public GetBlogUserStrategyBody() { }

        public GetBlogUserStrategyBody(Guid id)
        {
            Id = id;
        }

        public GetBlogUserStrategyBody(string[] tags)
        {
            Tags = tags;
        }

        public GetBlogUserStrategyBody(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }
}
