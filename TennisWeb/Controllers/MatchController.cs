using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.CF;

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
            ViewBag.Error = ViewData["ErrorMessage"] as string;

            ViewBag.SlotList = Services.SlotService.GetSlots();
            ViewBag.CoachList = Services.CoachService.GetCoaches();
            
            return View();
        }

        public ActionResult Store(Match match)
        {
            match.Status = true;
            var result = Services.MatchService.AddMatch(match);

            if (result)
            {
                TempData["SuccessMessage"] = "Match created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Error creating Match!";
                return View("Create");
            }
            
        }

        public ActionResult Match(int id)
        {
            try
            {
                var match = Services.MatchService.GetMatch(id);
                return View(match);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View();
            }
        }

    }
}