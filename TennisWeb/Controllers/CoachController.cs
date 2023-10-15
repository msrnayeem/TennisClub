using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TennisWeb.Controllers
{
    public class CoachController : Controller
    {
        // GET: Coach
        public ActionResult Index()
        {
            var coaches = Services.CoachService.GetCoaches();
            return View(coaches);
        }

        [HttpGet]
        public JsonResult DeleteCoach(int id)
        {
            try
            {
                var result = Services.CoachService.DeleteCoach(id);
                if(result == "deleted")
                {
                    return Json(new { success = true, message = "Coach deleted successfully" }, JsonRequestBehavior.AllowGet);
                }
                else if(result == "not found.")
                {
                    return Json(new { success = true, message = "Coach not Found" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {

                return Json(new { success = false, error = ex }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ChangeStatus(int id)
        {
            try
            {
                int playerId = Services.CoachService.GetCoach(id);

                if (playerId == 0)
                {
                    return Json(new { success = false, result = "User not Found" }, JsonRequestBehavior.AllowGet);
                }

                var result = Services.UserService.ChangeStatus(playerId);
                if (result == "changed")
                {
                    return Json(new { success = true, message = "Changes successfully" }, JsonRequestBehavior.AllowGet);
                }
                else if (result == "not found.")
                {
                    return Json(new { success = false, message = "User not Found" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = false, error = ex }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}