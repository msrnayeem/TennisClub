using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.Filter;

namespace TennisWeb.Controllers
{
   
    public class PlayerController : Controller
    {
        // GET: Player
        public ActionResult Index()
        {
            var players = Services.PlayerService.GetPlayers();
            return View(players);
        }

        
        [HttpGet]
        public JsonResult DeletePlayer(int id)
        {
            try
            {
                var result =Services.PlayerService.DeletePlayer(id);
                if(result == "Deleted")
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
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
                int playerId = Services.PlayerService.GetPlayer(id);

                if(playerId == 0)
                {
                    return Json(new { success = false , result = "User not Found" }, JsonRequestBehavior.AllowGet);
                }

                var result = Services.UserService.ChangeStatus(playerId);
                if (result == "changed")
                {
                    return Json(new { success = true, message = "Changes successfully" }, JsonRequestBehavior.AllowGet);
                }
                else if(result == "not found.")
                {
                    return Json(new { success = false, message = "User not Found" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message ="Failed to update" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {

                return Json(new { success = false, error = ex }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}