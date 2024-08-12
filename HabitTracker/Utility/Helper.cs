using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Utility
{
    public static class Helper
    {
        public static async Task<(String Id, string Username)> GetCurrentUserIdAsync(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext!.User?.Identity.IsAuthenticated == true)
            {
                var user = await userManager.GetUserAsync(httpContext.User);
                return (user!.Id, user!.UserName!);
            }
            return (string.Empty, string.Empty);
        }
    }
}
