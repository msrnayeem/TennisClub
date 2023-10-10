using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class CoachService
    {
        public static List<user> GetCoaches()
        {
            using (var db = new tennisEntities())
            {
                return db.users.Where(p => p.role == "coach").Include(p => p.coachInfoes).ToList();
            }
        }
    }
}