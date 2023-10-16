using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using TennisWeb.CF;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class AuthService
    {
        public static (string Role, string Status) AuthUser(string email, string password)
        {
            using (var db = new TennisContext())
            {
                
                var user = db.Users.SingleOrDefault(u => u.Email == email);

                if (user != null && user.Status == "inactive")
                {
                    return (null, "inactive");
                }
                else if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                {
                    return (user.Role, user.Status);
                }
               
                return (null, null);
                
            }
        }

        public static string GetRole(string Email)
        {
            using (var db = new TennisContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Email == Email);
                return user?.Role;
            }
        }

        public static int GetId(string Email)
        {
            using (var db = new TennisContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Email == Email);
                return user?.Id ?? 0;
            }
        }



        public static (int id, string role) GetIdRole(string Email)
        {
            using (var db = new TennisContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Email == Email);
                if (user != null)
                {
                    return (user.Id, user.Role);
                }
                else
                {
                    return (0, null);
                }
            }
        }

    }
}