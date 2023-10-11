using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.Models;

namespace TennisWeb.Controllers
{
    public class SlotController : Controller
    {
        // GET: Slot
        public ActionResult Index()
        {
            TempData["msg"] = TempData["SuccessMessage"] as string;

            var slots = Services.MatchService.GetSlots();
            return View(slots);
        }

        public ActionResult Create()
        {
            ViewBag.Error = ViewData["ErrorMessage"] as string;
            return View();
        }


        [HttpPost]
        public ActionResult Store(SlotModel slot)
        {
            if (ModelState.IsValid)
            {
                var res = Services.SlotService.CreateSlot(slot.name, slot.start, slot.end);

                if(res)
                {
                    TempData["SuccessMessage"] = "Slot created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Error creating slot";
                    return RedirectToAction("Create");
                }   
            }
            return View(slot);
            
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["SuccessMessage"] = "Id Not Found!";
                return RedirectToAction("Index");
            }
            else
            {
                var res = Services.SlotService.DeleteSlot(id);

                if(res)
                {
                    TempData["SuccessMessage"] = "Slot deleted successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["SuccessMessage"] = "Error deleting slot!";
                    return RedirectToAction("Index");
                }
            }
             
        }

    }
}