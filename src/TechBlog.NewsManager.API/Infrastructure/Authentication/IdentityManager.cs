using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TechBlog.NewsManager.API.Domain.Authentication;
using TechBlog.NewsManager.API.Domain.Entities;

namespace TechBlog.NewsManager.API.Infrastructure.Identity
{
    public class IdentityManager : IIdentityManager
    {
        private readonly UserManager<BlogUser> _userManager;

        public IdentityManager(UserManager<BlogUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateUserAsync(BlogUser user, string password, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return false;

            await _userManager.AddClaimsAsync(user, new[]
            {
                new Claim("BlogUserType", Enum.GetName(user.BlogUserType))
            });

            return result.Succeeded;
        }

        public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
