using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisWeb.CF;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class AuthService
    {
        public static (string Role, string Status) AuthUser(string Email, string Password)
        {
            using (var db = new TennisContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Email == Email && u.Password == Password);

                if (user != null && user.Status == "blocked")
                {
                    return ("blocked", null);
                }

                return user != null && user.Status != "block" ? (user.Role, user.Status) : (null, null);
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

       

    }
}