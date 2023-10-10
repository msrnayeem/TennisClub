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
        [Admin]
        public ActionResult UserList()
        {
            return View();
        }   
    }
}