using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using TennisWeb.CF;

namespace TennisWeb.Services
{
    public class FrontEndService
    {
        public static object GetInfo(string userEmail)
        {         
            var db = new TennisContext();

            User user = db.Users.SingleOrDefault(u => u.Email == userEmail);


            if(user.Role == "player")
            {
                PlayerInfo playerInfo = db.PlayerInfoes.Include("User").SingleOrDefault(p => p.UserId == user.Id);
                return playerInfo;
            }
            else if(user.Role == "coach")
            {
                CoachInfo coachInfo = db.CoachInfoes.Include("User").SingleOrDefault(c => c.UserId == user.Id);
                return coachInfo;
            }

            return null;
        }


        public static PlayerInfo GetPlayerInfo(int id)
        {
            
            var db = new TennisContext();
            PlayerInfo playerInfo = db.PlayerInfoes.SingleOrDefault(p => p.UserId == id);

            return playerInfo;
        }

        public static CoachInfo GetCoachInfo(int userId)
        {
            var db = new TennisContext();
            CoachInfo coachInfo = db.CoachInfoes.SingleOrDefault(c => c.Id == userId);
            return coachInfo;
        }
    }
}