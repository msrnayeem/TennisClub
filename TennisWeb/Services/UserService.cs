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

        public static bool AssignRole(int id, string role)
        {
            using (var db = new TennisContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id); // Here is the correction
                if (user != null)
                {
                    user.Role = role;
                    user.Status = "active";
                    return db.SaveChanges() > 0;
                }
                return false;
            }
        }


    }
}