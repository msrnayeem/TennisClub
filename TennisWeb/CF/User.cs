﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TennisWeb.Models;

namespace TennisWeb.CF
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }

        public virtual ICollection<CoachInfo> CoachInfoes { get; set; }
        public virtual ICollection<PlayerInfo> PlayerInfoes { get; set; }
    }
}