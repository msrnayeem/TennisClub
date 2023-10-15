using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public static List<CoachInfo> GetActivePlayers()
        {
            using (var db = new TennisContext())
            {
                return db.CoachInfoes
                   .Include(c => c.User)
                   .Where(c => c.User.Status == "active")
                   .ToList();
            }
        }

        public static string DeletePlayer(int id)
        {
            using (var db = new TennisContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var player = db.PlayerInfoes.FirstOrDefault(p => p.Id == id);
                        if (player != null)
                        {
                            var userId = player.UserId;
                            var user = db.Users.FirstOrDefault(u => u.Id == userId);
                            if (user != null)
                            {
                                db.Users.Remove(user);
                            }
                            db.PlayerInfoes.Remove(player);
                            db.SaveChanges();

                            transaction.Commit(); // If everything succeeds, commit the transaction
                            return "Deleted";
                        }
                        return "not found";
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or handle it as needed
                        transaction.Rollback(); // If an exception occurs, roll back the transaction
                        return "Error deleting player: " + ex.Message;
                    }
                }
            }
        }

        public static int GetPlayer(int id)
        {
            using (var db = new TennisContext())
            {
                var player = db.PlayerInfoes.FirstOrDefault(p => p.Id == id);
                return player != null ? player.UserId : 0; // Return 0 if player is not found
            }
        }



    }


}