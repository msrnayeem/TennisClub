using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.CF;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class UserService
    {
        public static List<User> GetUsers()
        {
            using (var db = new TennisContext())
            {
                return db.Users.Where( u => u.Role == "user").ToList();
            }
        }

        public static User GetUser(int id)
        {
            using (var db = new TennisContext())
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public static bool AddUser(User user)
        {
            using (var db = new TennisContext())
            {
                db.Users.Add(user);
                return db.SaveChanges() > 0;
            }
        }

        public static string ChangeStatus(int id)
        {
            try
            {
                using (var db = new TennisContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Id == id);
                    if (user != null)
                    {
                        if (user.Status == "active")
                        {
                            user.Status = "inactive";
                        }
                        else
                        {
                            user.Status = "active";
                        }

                        db.SaveChanges();
                        return "changed";
                    }
                    return "not found.";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, etc.
                return "Error changing status: " + ex.Message;
            }
        }


        public static bool AssignRole(int id, string role)
        {
            using (var db = new TennisContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var user = db.Users.FirstOrDefault(u => u.Id == id); // Here is the correction
                        if (user != null)
                        {
                            user.Role = role;
                            user.Status = "active";

                            if (role == "player")
                            {
                                var playerInfo = new PlayerInfo
                                {
                                    UserId = user.Id,
                                    // Set other properties for PlayerInfo as needed
                                };
                                db.PlayerInfoes.Add(playerInfo);
                            }
                            else if (role == "coach")
                            {
                                var coachInfo = new CoachInfo
                                {
                                    UserId = user.Id,
                                    // Set other properties for CoachInfo as needed
                                };
                                db.CoachInfoes.Add(coachInfo);
                            }

                            db.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        return false;
                    }
                    catch (Exception)
                    {
                        // Handle exception, log it if necessary
                        transaction.Rollback();
                        return false;
                    }
                }
            }

        }


        public static object GetDashboardInfo()
        {
            //count in user table where Role=user
            using(var db = new TennisContext())
            {
                var users = db.Users.Count();
                var pending = db.Users.Where(u => u.Status == "inactive").Count();
                var coaches = db.CoachInfoes.Count();
                var players = db.PlayerInfoes.Count();
                var slots = db.Slots.Count();
                var upcomingMatch = db.Matches.Where(m => m.Status == true).Count();
                var completedMatch = db.Matches.Where(m => m.Status == false).Count();
                
                return new
                {
                    users,
                    pending,
                    coaches,
                    players,
                    slots,
                    upcomingMatch,
                    completedMatch
                };
            }   
        }
    }
}