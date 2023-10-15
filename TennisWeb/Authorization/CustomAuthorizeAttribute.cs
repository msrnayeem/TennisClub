using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace TennisWeb.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string allowedRole;

        public CustomAuthorizeAttribute(string role)
        {
            allowedRole = role;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity is ClaimsIdentity user)
            {
                var roleClaim = user.FindFirst(ClaimTypes.Role);
                if (roleClaim != null && roleClaim.Value == allowedRole)
                {
                    return true;
                }
            }
            return false;
        }
    }
}