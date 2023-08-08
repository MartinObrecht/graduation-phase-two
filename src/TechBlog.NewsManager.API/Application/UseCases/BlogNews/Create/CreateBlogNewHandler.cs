using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using TechBlog.NewsManager.API.Domain.Authentication;
using TechBlog.NewsManager.API.Domain.Entities;
using TechBlog.NewsManager.API.Domain.Exceptions;
using TechBlog.NewsManager.API.Domain.Extensions;
using TechBlog.NewsManager.API.Domain.Logger;
using TechBlog.NewsManager.API.Domain.Responses;
using TechBlog.NewsManager.API.Infrastructure.Database;

namespace TechBlog.NewsManager.API.Application.UseCases.BlogNews.Create
{
    public static class CreateBlogNewHandler
    {
        public static string Route => "/api/v1/blognew";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };


        public static Delegate Handle => Action;
        internal static async Task<IResult> Action( ILoggerManager logger,
                                                    IMapper mapper,
                                                    IUnitOfWork unitOfWork,
                                                    CreateBlogNewRequest request, 
                                                    ClaimsPrincipal user,
                                                    IValidator<CreateBlogNewRequest> validator,
                                                    CancellationToken cancellationToken)
        {
            validator.ThrowIfInvalid(request);

            var response = new BaseResponse();

            var blogNew = mapper.Map<BlogNew>(request);
            blogNew.AuthorId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            await unitOfWork.BlogNew.AddAsync(blogNew, cancellationToken);

            if (!await unitOfWork.SaveChangesAsync(cancellationToken))
            {
                logger.LogWarning("Error creating blog new", ("Title", request.Title));

                throw new InfrastructureException("An unexpected error ocurred");
            }

            return Results.Ok(response.AsSuccess());
        }
        
    }
}