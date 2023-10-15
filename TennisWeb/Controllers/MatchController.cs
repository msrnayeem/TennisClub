using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.CF;
using WebGrease.Css.Ast.Selectors;

namespace TennisWeb.Controllers
{
    public class MatchController : Controller
    {
        // GET: Match
        public ActionResult Index()
        {
            TempData["msg"] = TempData["SuccessMessage"] as string;

            var matches = Services.MatchService.GetMatches();
            
            return View(matches);
        }

        public ActionResult Create()
        {
            TempData["msg"] = TempData["SuccessMessage"] as string;

            ViewBag.SlotList = Services.SlotService.GetSlots();
            ViewBag.CoachList = Services.CoachService.GetCoaches();
            
            return View();
        }

        public ActionResult Store(Match match)
        {
            match.Status = false;
            string resultMessage = Services.MatchService.AddMatch(match);

            // Check if the resultMessage starts with "Error:"
            if (resultMessage.StartsWith("Error:"))
            {
                // If there was an error, store the error message in TempData
                TempData["SuccessMessage"] = resultMessage;
            }
            else
            {
                // If there was no error, you can store a success message if needed
                TempData["SuccessMessage"] = resultMessage;
            }
            return RedirectToAction("Create");

        }

        public ActionResult Match(int id)
        {
            var match = Services.MatchService.GetMatchInformation(id);
            return View(match);
        }

        public ActionResult DeleteMatch(int id)
        {
            var resultMessage = Services.MatchService.DeleteMatch(id);
           
            if (resultMessage.StartsWith("Error:"))
            {
                TempData["SuccessMessage"] = resultMessage;
            }
            else
            {
                TempData["SuccessMessage"] = resultMessage;
            }
            return RedirectToAction("Index");
        }

        public ActionResult UpdateMatch(int id)
        {
            var resultMessage = Services.MatchService.UpdateMatch(id);

            if (resultMessage.StartsWith("Error:"))
            {
                TempData["SuccessMessage"] = resultMessage;
            }
            else
            {
                TempData["SuccessMessage"] = resultMessage;
            }
            return RedirectToAction("Index");
        }


        //apis for adding slot
        [HttpGet]
        public JsonResult GetBookedSlots(DateTime date)
        {
            try
            {
                var freeSlots = Services.SlotService.GetFreeSlots(date);

                return Json(new { success = true, slots = freeSlots }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine(ex.Message);
                return Json(new { success = false, error = "An error occurred while fetching slots." }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult GetFreeCoaches(DateTime date, int id)
        {
            try
            {
                var freeCoaches = Services.MatchService.GetFreeCoaches(date, id);
                if(freeCoaches.Count == 0)
                {
                    return Json(new { success = false, error = "No free coaches available." }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, coaches = freeCoaches }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine(ex.Message);
                return Json(new { success = false, error = "An error occurred while fetching coaches." }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}