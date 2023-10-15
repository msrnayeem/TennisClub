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
        public static (string Role, string Status) AuthUser(string Email, string Password)
        {
            using (var db = new TennisContext())
            {
                var password = Crypto.HashPassword(Password);
                var user = db.Users.SingleOrDefault(u => u.Email == Email && u.Password == password);

                if (user != null && user.Status == "inactive")
                {
                    return (null, "inactive");
                }

                return user != null && user.Status != "inactive" ? (user.Role, user.Status) : (null, null);
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