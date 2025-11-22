using System.Security.Claims;

namespace ML_API.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            var sub = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? httpContext.User.FindFirstValue(ClaimTypes.Name)
                      ?? httpContext.User.FindFirstValue("sub");

            if (sub is null || !Guid.TryParse(sub, out var userId))
                throw new UnauthorizedAccessException("UserId not found in token.");

            return userId;
        }
    }
}
