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
                return db.Users.ToList();
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

        public static bool ChnageStatus(int id)
        {
            using (var db = new TennisContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                   if(user.Status == "active")
                    {
                        user.Status = "inactive";
                    }
                    else
                    {
                        user.Status = "active";
                    }   
                    return db.SaveChanges() > 0;
                }
                return false;
            }
        }

        public static Exception AssignRole(string id, string role)
        {
            try
            {
                using (var db = new TennisContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Id == Convert.ToInt32(id));
                            if (user != null)
                            {
                                user.Role = role;

                                // Reattach the modified entity and mark the role property as modified
                                db.Entry(user).State = EntityState.Modified;
                                db.SaveChanges();

                                transaction.Commit();
                                return new Exception("updated");
                            }
                            else
                            {
                                // Return an exception indicating user not found
                                return new Exception("User not found");
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            // Log the exception for debugging
                            // ...

                            // Return the caught exception to indicate failure
                            return ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions here
                // Log the exception for debugging
                // ...

                // Return the caught exception to indicate failure
                return ex;
            }

        }

    }
}