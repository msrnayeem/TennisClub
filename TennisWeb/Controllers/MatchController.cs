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
            TempData["msg"] = TempData["ErrorMessage"] as string;

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
                TempData["ErrorMessage"] = resultMessage;
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




        //apis for adding slot
        [HttpGet]
        public JsonResult GetBookedSlots(DateTime date)
        {
            try
            {
                var allSlots = Services.SlotService.GetAllSlots();

                // Static list of booked slot IDs
                var bookedSlotIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

                var freeSlots = allSlots
            .Where(slot => !bookedSlotIds.Contains(slot.Id))
            .Select(slot => new
            {
                Id = slot.Id,
                Name = slot.Name,
                Start = slot.Start,
                End = slot.End,
                Status = slot.Status
            })
            .ToList();

                return Json(freeSlots, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine(ex.Message);
                return Json(new { error = "An error occurred while fetching slots." });
            }
        }

    }
}