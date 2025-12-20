using System.Security.Claims;

namespace EndPoint.Site.Utilities
{
    public static class ClaimUtilities
    {
        public static long? GetUserId(ClaimsPrincipal user)
        {
            ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
            if(claimsIdentity.IsAuthenticated)
                return long.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            return null;
        }
    }
}
