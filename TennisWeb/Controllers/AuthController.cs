using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TennisWeb.CF;
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
            else if(role == null && status == null)
            {
                TempData["msg"] = "Email/Password not matched";
            }
            else if(role == null && status == "inactive")
            {
                TempData["msg"] = "Your account is inactive";
            }
            else
            {
                TempData["msg"] = "Something went wrong";
            }   
            
            return RedirectToAction("Login");
        }


        public ActionResult Logout()
        {
            Session["Role"] = null;
            Session["Email"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Store(User user) { 

            if (ModelState.IsValid){

                var newUser = new CF.User
                {
                    Email = user.Email,
                    Password = Crypto.HashPassword(user.Password),
                    Role = "user",
                    Status = "inactive"
                };
                if (UserService.AddUser(newUser))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    return RedirectToAction("Register");
                }
             }

            return View("Register", user);

        }
    }
}