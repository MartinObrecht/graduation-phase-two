using System.ComponentModel;

namespace TechBlog.NewsManager.API.Domain.Responses
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
