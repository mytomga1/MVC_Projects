using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace BaoOnline.Extensions
{
    public static class IndentityExtensions
    {
        public static string GetAccoutID(this IIdentity identity) {

            var claim = ((ClaimsIdentity)identity).FindFirst("AccountID");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetRoleID(this IIdentity identity)
        {

            var claim = ((ClaimsIdentity)identity).FindFirst("RoleID");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetCredits(this IIdentity identity)
        {

            var claim = ((ClaimsIdentity)identity).FindFirst("VipCredits");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetAvatar(this IIdentity identity)
        {

            var claim = ((ClaimsIdentity)identity).FindFirst("Avatar");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {

            var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
