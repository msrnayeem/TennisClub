using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisWeb.CF;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class MatchService
    {
        public static List<Match> GetMatches()
        {
            using (var db = new TennisContext())
            {
                return db.Matches.Include("Slot").Include("CoachInfo").ToList();
            }
        }

        public static bool AddMatch(Match match)
        {
            using (var db = new TennisContext())
            {
                db.Matches.Add(match);
                
                return db.SaveChanges() > 0;
            }
        }

        public static Match GetMatch(int id)
        {
            using (var db = new TennisContext())
            {
                var result = db.Matches.Include("Slot").Include("CoachInfo").FirstOrDefault(m => m.Id == id);
                return result ?? throw new Exception("Match not found!");
            }
        }
    }
}