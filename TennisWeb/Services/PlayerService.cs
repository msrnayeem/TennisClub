using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class PlayerService
    {
        public static List<user> GetPlayers()
        {
            using (var db = new tennisEntities())
            {
                return db.users.Where(p => p.role == "player").Include(p => p.playerInfoes).ToList();
            }
        }
    }
}