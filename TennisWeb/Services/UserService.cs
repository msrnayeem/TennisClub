using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class UserService
    {
        public static List<user> GetUsers()
        {
            using (var db = new tennisEntities())
            {
                return db.users.ToList();
            }
        }

        public static user GetUser(int id)
        {
            using (var db = new tennisEntities())
            {
                return db.users.FirstOrDefault(u => u.id == id);
            }
        }

        public static bool AddUser(user user)
        {
            using (var db = new tennisEntities())
            {
                db.users.Add(user);
                return db.SaveChanges() > 0;
            }
        }

        public static bool ChnageStatus(int id)
        {
            using (var db = new tennisEntities())
            {
                var user = db.users.FirstOrDefault(u => u.id == id);
                if (user != null)
                {
                   if(user.status == "active")
                    {
                        user.status = "inactive";
                    }
                    else
                    {
                        user.status = "active";
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
                using (var db = new tennisEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var user = db.users.AsNoTracking().FirstOrDefault(u => u.id == Convert.ToInt32(id));
                            if (user != null)
                            {
                                user.role = role;

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