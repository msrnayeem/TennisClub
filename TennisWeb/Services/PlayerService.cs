using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TennisWeb.CF;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class PlayerService
    {
        public static List<User> GetPlayers()
        {
            using (var db = new TennisContext())
            {
                return db.Users.Where(p => p.Role == "player").Include(p => p.PlayerInfoes).ToList();
            }
        }
    }
}