using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TennisWeb.CF;

namespace TennisWeb.Services
{
    public class MatchPlayerService
    {
        public static bool AddMatchPlayer(List<MatchPlayer> matchPlayers)
        {
            using (var db = new TennisContext())
            {
                foreach (var matchPlayer in matchPlayers)
                {
                    db.MatchPlayers.Add(matchPlayer);
                }
                db.SaveChanges();
                return true;
            }
            
        }

        public static string DeleteMatchPlayer(int matchId,int playerId)
        {
            try
            {
                using (var db = new TennisContext())
                {
                    var matchPlayer = db.MatchPlayers.FirstOrDefault(m=> m.MatchId == matchId && m.PlayerId == playerId);

                    if (matchPlayer != null)
                    {
                        db.MatchPlayers.Remove(matchPlayer);
                        db.SaveChanges();
                        return "Player Removed from this match"; // Deletion successful
                    }

                    return "Error: MatchPlayer with the given ID was not found";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}"; // Handle and return the specific exception message
            }
        }



        public static List<MatchPlayer> GetMatchInfo(int id)
        {
            using (var db = new TennisContext())
            {
                var result = db.MatchPlayers
                    .Include("PlayerInfo")
                    .Include("Match.Slot")
                    .Include("Match.CoachInfo")
                    .Where(x => x.MatchId == id)
                    .ToList();
                return result;

            }
        }



        public static List<int> GetSelected(int id)
        {
            using (var db = new TennisContext())
            {
                return db.MatchPlayers.Where(x => x.MatchId == id).Select(x => x.PlayerId).ToList();
            }
        }

    }
}