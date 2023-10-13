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