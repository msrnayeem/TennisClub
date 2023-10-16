using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using TennisWeb.CF;
using TennisWeb.Filter;

namespace TennisWeb.Controllers
{
    public class FrontEndController : Controller
    {
        // GET: FrontEnd
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        
        public ActionResult Contact()
        {
            return View();
        }

        [Authorize]
        public ActionResult FrontProfile()
        {

            if (Request.IsAuthenticated)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    string userEmail = User.Identity.Name;
  
                    object info = Services.FrontEndService.GetInfo(userEmail);
                   
                    if(info != null)
                    {
                        ViewBag.Info = info;
                    }
                    else
                    {
                        ViewBag.Info = null;
                    }

                }
                return View();

            }
            else
            {
                return RedirectToAction("Logout", "Auth");
            }
        }

        [Authorize]
        public ActionResult Upcoming()
        {

            if (Request.IsAuthenticated)
            {
                List<Match> matches = new List<Match>();

                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    string userEmail = User.Identity.Name;

                    var (id, role) = Services.AuthService.GetIdRole(userEmail);

                    //user id= 9, coach id=3 email=msrnayeemm@gmail.com

                    if(role == "coach")
                    {
                        int coachId = Services.CoachService.GetCoachId(id);
                        matches = Services.CoachService.GetCoachMatchesList(coachId);
                    }
                    else
                    {
                        int playerId = Services.PlayerService.GetPlayerId(id);
                        return RedirectToAction("MatchForPlayer",new{ id =playerId });
                    }
                }
                return View(matches);

            }
            else
            {
                return RedirectToAction("Logout", "Auth");
            }
        }

        public ActionResult MatchForPlayer(int id)
        {
            var info = Services.PlayerService.GetPlayersMatchesList(id);
            return View(info);
        }

       
        public ActionResult MatchDetails(int id)
        {
            try
            {
                TempData["msg"] = TempData["SuccessMessage"] as string;

                var players = Services.PlayerService.GetActivePlayers();
                var playerSelectList = new SelectList(players, "Id", "Name");

                ViewBag.allPlayers = playerSelectList;



                var selectedPlayers = Services.MatchPlayerService.GetSelected(id);
                ViewBag.selectedPlayers = selectedPlayers;

                var playersInfo = Services.MatchService.GetMatchInformation(id);


                return View(playersInfo);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View();
            }
        }


        [HttpPost]
        public ActionResult UpdateProfile(FormCollection form)
        {
            string role = form["role"];
            int id = Convert.ToInt32(form["id"]);
            if(role == "player")
            {
                //return to player update
                return RedirectToAction("PlayerUpdate", new { id });
            }
            else
            {
                return RedirectToAction("CoachUpdate", new {  id });
            }

        }


        public ActionResult PlayerUpdate(int id)
        {
            var info = Services.PlayerService.GetPlayerInfo(id);
            return View(info);
        }

        //coach update
        public ActionResult CoachUpdate(int id)
        {
            var info = Services.CoachService.GetCoachInfo(id);

            if (info != null)
            {
                return View(info);
            }
            else
            {
                // Handle the case where info is null, perhaps redirect to an error page or display an error message.
                return RedirectToAction("Logout", "Auth");
            }
        }


    }
}