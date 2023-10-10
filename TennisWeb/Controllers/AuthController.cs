using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TennisWeb.Models;
using TennisWeb.Services;

namespace TennisWeb.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            TempData["ErrorMessage"] = Request.QueryString["error"];
            return View();
        }

        [HttpPost]
        public ActionResult LoginSubmit(FormCollection form)
        {
            var (role, status) = AuthService.AuthUser(form["Email"], form["Password"]);

            if (role != null && status != null)
            {
                Session["Role"] = role;
                Session["Email"] = form["Email"];
                return RedirectToAction("Dashboard", "User");
            }           
            else if (status == "blocked")
            {
                TempData["ErrorMessage"] = "User is blocked, contact admin";
                return RedirectToAction("Login"); 
            }
            TempData["ErrorMessage"] = "Invalid email or password";
            return RedirectToAction("Login");
        }
    }
}