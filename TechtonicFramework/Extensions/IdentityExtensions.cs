using System;
using System.Security.Claims;
using System.Security.Principal;

namespace TechtonicFramework.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUsername(this IPrincipal user)
        {
            return user.Identity?.Name ?? throw new UnauthorizedAccessException();
        }

        public static string GetUserId(this IPrincipal user)
        {
            var claimsPrincipal = user as ClaimsPrincipal;
            return claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? throw new UnauthorizedAccessException();
        }
    }
}
