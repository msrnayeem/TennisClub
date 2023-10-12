﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TennisWeb.CF;


namespace TennisWeb.Services
{
    public class CoachService
    {
        public static List<CoachInfo> GetCoaches()
        {
            using (var db = new TennisContext())
            {
                return db.CoachInfoes.Include(p => p.User).ToList();
            }
        }
    }
}