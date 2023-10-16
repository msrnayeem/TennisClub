using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.Services;

namespace TennisWeb.Filter
{
    public class AdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Auth/Logout");
                return;
            }
            var role = AuthService.GetRole(HttpContext.Current.User.Identity.Name);
            if (role != "admin")
            {
                // Store the previous URL in the session
                string url = filterContext.HttpContext.Request.UrlReferrer.ToString();
                filterContext.Result = new RedirectResult(url);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

    }
}
