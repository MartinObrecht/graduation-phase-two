using Microsoft.AspNetCore.Mvc;
using TechBlog.NewsManager.API.Domain.Authentication;
using TechBlog.NewsManager.API.Domain.Logger;
using TechBlog.NewsManager.API.Domain.Responses;

namespace TechBlog.NewsManager.API.Application.UseCases.Authentication.Login
{
    public static class LoginHandler
    {
        public static string Route => "/api/v1/auth";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        internal static async Task<IResult> Action(ILoggerManager logger, IIdentityManager identityManager, LoginRequest request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Begin Login", ("username", request.Username));

            var response = new BaseResponseWithValue<AccessTokenModel>();

            var user = await identityManager.GetByEmailAsync(request.Username, cancellationToken);

            if (!user.Exists)
            {
                logger.LogWarning("Officer don't exists", ("username", request.Username));
                    
                return Results.BadRequest(response.AsError(ResponseMessage.InvalidCredentials));
            }

            var accessToken = await identityManager.AuthenticateAsync(user, request.Password, cancellationToken,("name", user.Name));

            if (!accessToken.Valid)
            {
                logger.LogWarning("Invalid credentials", ("username", request.Username));

                return Results.BadRequest(response.AsError(ResponseMessage.InvalidCredentials));
            }

            logger.LogDebug("Success Login", ("username", request.Username));

            return Results.Ok(response.AsSuccess(accessToken));
        }
    }
}
