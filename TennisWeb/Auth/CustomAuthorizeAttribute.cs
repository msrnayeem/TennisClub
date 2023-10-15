using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TennisWeb.Services;

namespace TennisWeb.Auth
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
            var authService = new AuthService();
            var user = httpContext.User.Identity.Name;
            var customPrincipal = authService.AuthenticateUser(user, ""); // Empty password because passwords are not hashed

            if (customPrincipal != null && customPrincipal.IsInRole(allowedRole))
            {
                return true;
            }

            return false;
        }
    }


}