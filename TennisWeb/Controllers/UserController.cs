using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.Filter;

namespace TennisWeb.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var users = Services.UserService.GetUsers();
            return View(users);
        }   
    }
}