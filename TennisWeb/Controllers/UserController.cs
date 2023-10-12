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

        public ActionResult ChnageStatus(int id)
        {
            var user = Services.UserService.ChnageStatus(id);

            if(user)
            {
                TempData["SuccessMessage"] = "Status changed successfully!";
                return RedirectToAction("UserList");
            }
            else
            {
                TempData["SuccessMessage"] = "Error changing status!";
                return RedirectToAction("UserList");
            }
        }

        
        
    }
}