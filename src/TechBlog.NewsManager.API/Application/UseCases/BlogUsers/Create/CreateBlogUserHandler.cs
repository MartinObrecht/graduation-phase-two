using AutoMapper;
using FluentValidation;
using TechBlog.NewsManager.API.Domain.Authentication;
using TechBlog.NewsManager.API.Domain.Entities;
using TechBlog.NewsManager.API.Domain.Extensions;
using TechBlog.NewsManager.API.Domain.Logger;
using TechBlog.NewsManager.API.Domain.Responses;

namespace TechBlog.NewsManager.API.Application.UseCases.BlogUsers.Create
{
    public static class CreateBlogUserHandler
    {
        public static string Route => "/api/v1/users";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        internal static async Task<IResult> Action(IIdentityManager identityManager,
                                                   ILoggerManager logger,
                                                   IMapper mapper,
                                                   IValidator<CreateBlogUserRequest> validator,
                                                   CreateBlogUserRequest request,
                                                   CancellationToken cancellationToken)
        {
            logger.LogDebug("Begin creating user", ("Email", request.Email));

            validator.ThrowIfInvalid(request);

            var response = new BaseResponse();

            var blogUser = mapper.Map<BlogUser>(request);

            if (await identityManager.ExistsAsync(blogUser.Email, cancellationToken))
            {
                logger.LogInformation("User already exists", ("Email", request.Email));

                return Results.BadRequest(response.AsError(ResponseMessage.UserAlreadyExists));
            }

            logger.LogDebug("User don't exists, creting new", ("Email", request.Email));

            if (!await identityManager.CreateUserAsync(blogUser, request.Password, cancellationToken))
            {
                logger.LogWarning("Error creating user", ("Email", request.Email));

                return Results.BadRequest(response.AsError(ResponseMessage.InvalidInformation));
            }

            logger.LogDebug("Success creating user", ("Email", request.Email));

            return Results.Ok(response.AsSuccess());
        }
    }
}
