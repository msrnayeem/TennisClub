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
        public static List<User> GetCoaches()
        {
            using (var db = new TennisContext())
            {
                return db.Users.Where(p => p.Role == "coach").Include(p => p.CoachInfoes).ToList();
            }
        }
    }
}