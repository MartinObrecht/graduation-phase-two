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
        [Description("User already exists")]
        UserAlreadyExists = 4,
        [Description("Error creating user")]
        ErrorCreatingUser = 5,
        [Description("Invalid email")]
        InvalidEmail = 6,
        [Description("Invalid name")]
        InvalidName = 7,
        [Description("Invalid password")]
        InvalidPassword = 8,
        [Description("Invalid user type")]
        InvalidUserType = 9,
        [Description("Invalid information")]
        InvalidInformation = 10
    }
}
