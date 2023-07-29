using System.ComponentModel;

namespace TechBlog.NewsManager.API.Application.Contracts.Responses
{
    public enum ResponseMessage
    {
        [Description("An error ocurred, try again later")]
        GenericError = 0,
        [Description("Success")]
        Success = 1,
        [Description("User is not authenticated")]
        UserIsNotAuthenticated = 2,
    }
}
