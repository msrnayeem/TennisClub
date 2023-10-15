using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TennisWeb.CF;


namespace TennisWeb.Services
{
    public class CoachService
    {
        public static List<CoachInfo> GetCoaches()
        {
            using (var db = new TennisContext())
            {
                return db.CoachInfoes
                   .Include(c => c.User).ToList();
            }
        }

        public static List<CoachInfo> GetActiveCoaches()
        {
            using (var db = new TennisContext())
            {
                return db.CoachInfoes
                   .Include(c => c.User)
                   .Where(c => c.User.Status == "active")
                   .ToList();
            }
        }

    }
}