using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TennisWeb.CF;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class MatchService
    {
        public static List<CF.Match> GetMatches()
        {
            using (var db = new TennisContext())
            {
                return db.Matches.Include("Slot").Include("CoachInfo").ToList();
            }
        }

        public static bool AddMatch(CF.Match match)
        {
            using (var db = new TennisContext())
            {
                db.Matches.Add(match);
                
                return db.SaveChanges() > 0;
            }
        }

        public static List<MatchPlayer>  GetMatchInfo(int id)
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
    }
}