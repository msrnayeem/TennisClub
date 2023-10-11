using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TennisWeb.Controllers
{
    public class SlotController : Controller
    {
        // GET: Slot
        public ActionResult Index()
        {
            var slots = Services.MatchService.GetSlots();
            return View(slots);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Store(FormCollection form)
        {
            var name = form["name"];
            var start = form["start"];
            var end = form["end"];

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                var result = Services.SlotService.CreateSlot(name, start, end);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Invalid form data. Please fill in all fields.";
                    return View("Error"); // Redirect to an error page indicating validation error
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid form data. Please fill in all fields.";
                return View("Error"); // Redirect to an error page indicating validation error
            }
        }

    }
}