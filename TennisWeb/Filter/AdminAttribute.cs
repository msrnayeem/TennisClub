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
            if (HttpContext.Current.Session["Email"] == null)
            {
                string errorMessage = "You do not have permission to access this page.";
                filterContext.Result = new RedirectResult("~/Auth/Login?error=" + HttpUtility.UrlEncode(errorMessage));
                return;
            }

            try
            {
                var role = AuthService.GetRole(HttpContext.Current.Session["Email"].ToString());
                if (role != "admin")
                {
                    var redirectUrl = $"~/Auth/Login?error=You do not have permission to access this page.";
                    filterContext.Result = new RedirectResult(redirectUrl);
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Error"); // Example: Redirect to an error page
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
