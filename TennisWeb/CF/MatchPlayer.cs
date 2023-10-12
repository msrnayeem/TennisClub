using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class MatchPlayer
    {
        public int id { get; set; } // Primary key
        public int MatchId { get; set; } // Foreign key to Match entity
        public int PlayerId { get; set; } // Foreign key to PlayerInfo entity

        // Navigation properties
        public virtual Match Match { get; set; }
        public virtual PlayerInfo Player { get; set; }
    }
}