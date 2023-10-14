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
        public static List<PlayerInfo> GetPlayers()
        {
            using (var db = new TennisContext())
            {
                return db.PlayerInfoes.Include(p => p.User).ToList();
            }
        }

        
    }
}