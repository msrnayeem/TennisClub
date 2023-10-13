using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.CF;

namespace TennisWeb.Controllers
{
    public class MatchPlayerController : Controller
    {
        // GET: MatchPlayer
        public ActionResult Index(int id)
        {
            try
            {
                var players = Services.PlayerService.GetPlayers();
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
            if(res)
                return RedirectToAction("Index","MatchPlayer", new { id = matchId });

            return RedirectToAction("Index");
            
        }       
    }
}