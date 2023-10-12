using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisWeb.CF;
using TennisWeb.Models;

namespace TennisWeb.Services
{
    public class MatchService
    {
        public static List<Slot> GetSlots()
        {
            using (var db = new TennisContext())
            {
                return db.Slots.ToList();
            }
        }
    }
}