using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TennisWeb.Filter;

namespace TennisWeb.Controllers
{
   
    public class UserController : Controller
    {
        [HttpGet]
        public JsonResult AssignRole(int id, string role)
        {
            
                // Your logic to assign the role based on the id and role parameters
                // ...
                var  user = Services.UserService.AssignRole(id, role);

                
                return Json(new { success = true, message = user }, JsonRequestBehavior.AllowGet);
            
            
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult UserList()
        {
            TempData["msg"] = TempData["SuccessMessage"] as string;

            var users = Services.UserService.GetUsers();
            return View(users);
        }
        
        public ActionResult UserDetail(int id)
        {
            var user = Services.UserService.GetUser(id);
            return View(user);
        }


        [HttpGet]
        public JsonResult DashboardInfo()
        {
            try
            {
                var info = Services.UserService.GetDashboardInfo();
                
                return Json(new { success = true, result = info }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine(ex.Message);
                return Json(new { success = false, error = "An error occurred while fetching coaches." }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}