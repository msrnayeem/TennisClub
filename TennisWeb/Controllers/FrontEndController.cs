using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TennisWeb.Filter;

namespace TennisWeb.Controllers
{
    public class FrontEndController : Controller
    {
        // GET: FrontEnd
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    ViewBag.UserEmail = ticket.UserData; // User email from the ticket

                }
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        
        public ActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public ActionResult FrontProfile()
        {
           // string userEmail = (string)HttpContext.Current.Session["UserEmail"];
            return View();
        }
    }
}