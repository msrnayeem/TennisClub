using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class AuthService
    {
        public static (string Role, string Status) AuthUser(string Email, string Password)
        {
            using (var db = new tennisEntities())
            {
                var user = db.users.SingleOrDefault(u => u.email == Email && u.password == Password);

                if (user != null && user.status == "blocked")
                {
                    return ("blocked", null);
                }

                return user != null && user.status != "block" ? (user.role, user.status) : (null, null);
            }
        }

        public static string GetRole(string Email)
        {
            using (var db = new tennisEntities())
            {
                var user = db.users.SingleOrDefault(u => u.email == Email);
                return user?.role;
            }
        }

    }
}