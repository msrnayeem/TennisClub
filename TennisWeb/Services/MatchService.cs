using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TennisWeb.CF;
using TennisWeb.Models;
using Match = TennisWeb.CF.Match;
using System.Data.Entity;

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

        public static Match GetMatchInformation(int id)
        {
            using(var db = new TennisContext())
            {
                return db.Matches
                    .Include(m => m.Slot)
                 .Include(m => m.CoachInfo)
                 .Include(m => m.MatchPlayers.Select(mp => mp.PlayerInfo)) // Include MatchPlayers and their related PlayerInfo entities
                 .Where(m => m.Id == id)
                 .SingleOrDefault();
            }
        }

        
    }
}