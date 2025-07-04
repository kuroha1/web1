﻿using System.Security.Claims;
using System.Security.Principal;

namespace online.Extention
{
    public static class IdentityExtention
    {
        public static string GetAccountID(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("AccountID");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetRoleID (this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("RoleID");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserName");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetAvatar(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Avatar");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal,string claimType)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
