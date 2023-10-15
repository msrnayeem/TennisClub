using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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


        public static string DeleteCoach(int id)
        {
            using (var db = new TennisContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var coach = db.CoachInfoes.FirstOrDefault(p => p.Id == id);
                        if (coach != null)
                        {
                            // Manually delete related Matches records with the corresponding CoachId
                            var relatedMatches = db.Matches.Where(m => m.CoachId == coach.Id).ToList();
                            foreach (var match in relatedMatches)
                            {
                                db.Matches.Remove(match);
                            }

                            var userId = coach.UserId;
                            var user = db.Users.FirstOrDefault(u => u.Id == userId);
                            if (user != null)
                            {
                                db.Users.Remove(user);
                            }
                            db.CoachInfoes.Remove(coach);
                            db.SaveChanges();

                            transaction.Commit(); // If everything succeeds, commit the transaction
                            return "deleted";
                        }
                        return "not found";
                    }
                    catch (DbUpdateException ex)
                    {
                        // Log the exception or handle it as needed
                        transaction.Rollback(); // If an exception occurs, roll back the transaction
                        return "Error deleting Coach: " + ex.GetBaseException().Message;
                    }
                }
            }
        }



        public static int GetCoach(int id)
        {
            using (var db = new TennisContext())
            {
                var coach = db.CoachInfoes.FirstOrDefault(p => p.Id == id);
                return coach != null ? coach.UserId : 0; // Return 0 if player is not found
            }
        }

    }
}