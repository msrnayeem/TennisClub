using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.Services;

namespace TennisWeb.Filter
{
    public class CoachAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Email"] == null)
            {
                filterContext.Result = new RedirectResult("~/Auth/Login");
                return;
            }

            try
            {
                var role = AuthService.GetRole(HttpContext.Current.Session["Email"].ToString());
                if (role != "coach")
                {
                    // Add an error message in the query parameter
                    var redirectUrl = $"~/Auth/Login?error=You do not have permission to access this page.";
                    filterContext.Result = new RedirectResult(redirectUrl);
                }
            }
            catch (Exception)
            {
                // Handle exceptions that might occur during AuthService.GetRole method call
                // Log the exception, redirect to an error page, or handle it as appropriate for your application
                filterContext.Result = new RedirectResult("~/Error"); // Example: Redirect to an error page
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
