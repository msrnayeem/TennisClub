using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TennisWeb.CF;
using TennisWeb.Filter;

namespace TennisWeb.Controllers
{
    
    public class MatchPlayerController : Controller
    {
        
        public ActionResult Index(int id)
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

        //create a new match player
        [HttpPost]
        public ActionResult Store(FormCollection form)
        {
            // Get match ID from form data
            // Get match ID from form data
            int matchId = Convert.ToInt32(form["matchId"]);

            // Get selected player IDs from form data
            string[] selectedPlayerIds = form.GetValues("selectedPlayers[]");

            // Create a list of MatchPlayer objects
            List<MatchPlayer> matchPlayers = new List<MatchPlayer>();

            foreach (var playerId in selectedPlayerIds)
            {
                int playerIdInt = Convert.ToInt32(playerId);

                // Create MatchPlayer object and set matchId and playerId
                MatchPlayer matchPlayer = new MatchPlayer
                {
                    MatchId = matchId,
                    PlayerId = playerIdInt
                };

                // Add the MatchPlayer object to the list
                matchPlayers.Add(matchPlayer);
            }

            var res = Services.MatchPlayerService.AddMatchPlayer(matchPlayers);
            if (res)
            {
                TempData["SuccessMessage"] = "Match created successfully!";
                return RedirectToAction("Index", "MatchPlayer", new { id = matchId });
            }

            TempData["SuccessMessage"] = "Failed to Create Match!";
            return RedirectToAction("Index", "MatchPlayer", new { id = matchId });


        }

        public ActionResult Delete(int matchId, int playerId)
        {
            try
            {
                var res = Services.MatchPlayerService.DeleteMatchPlayer(matchId,playerId);
                TempData["SuccessMessage"] = res;
                return RedirectToAction("Index", "MatchPlayer", new { id = matchId });
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Index", "MatchPlayer", new { id = matchId });
            }
        }
    }
}