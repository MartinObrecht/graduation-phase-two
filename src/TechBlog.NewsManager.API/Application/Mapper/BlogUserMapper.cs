using AutoMapper;
using TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create;
using TechBlog.NewsManager.API.Domain.Entities;

namespace TechBlog.NewsManager.API.Application.Mapper
{
    public class BlogUserMapper : Profile
    {
        public BlogUserMapper()
        {
            CreateMap<CreateBlogUserRequest, BlogUser>().AfterMap((request, entity) =>
            {
                entity.UserName = request.Email;
                entity.CreatedAt = DateTime.Now;
                entity.LastUpdateAt = DateTime.Now;
                entity.Enabled = true;
            });
        }
    }
}
