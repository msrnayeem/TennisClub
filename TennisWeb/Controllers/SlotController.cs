using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.CF;
using TennisWeb.Filter;
using TennisWeb.Models;

namespace TennisWeb.Controllers
{
    
    public class SlotController : Controller
    {
        // GET: Slot
        public ActionResult Index()
        {
            TempData["msg"] = TempData["SuccessMessage"] as string;

            var slots = Services.SlotService.GetSlots();
            return View(slots);
        }

        public ActionResult Create()
        {
            ViewBag.Error = ViewData["ErrorMessage"] as string;
            return View();
        }


        [HttpPost]
        public ActionResult Store(Slot slot)
        {
            var result = Services.SlotService.CreateSlot(slot.Name, slot.Start, slot.End);
            if(result)
            {
                TempData["SuccessMessage"] = "Slot created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Error creating slot!";
                return RedirectToAction("Create");
            }
            
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
                var resultMessage = Services.SlotService.DeleteSlot(id);

                if (resultMessage.StartsWith("Error:"))
                {
                    // If there was an error, store the error message in TempData
                    TempData["SuccessMessage"] = resultMessage;
                }

                
                TempData["SuccessMessage"] = "Slot deleted successfully!";
                return RedirectToAction("Index");
                
                
            }
             
        }


        [HttpGet]
        public JsonResult ChangeStatus(int id)
        {

            // Your logic to assign the role based on the id and role parameters
            // ...
            var result = Services.SlotService.ChangeStatus(id);


            return Json(new { success = true, message = result }, JsonRequestBehavior.AllowGet);


        }

    }
}