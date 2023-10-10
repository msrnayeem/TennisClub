using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}