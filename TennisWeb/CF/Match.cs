using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class Match
    {
        public int Id { get; set; } // Primary key
        public int SlotId { get; set; } // Foreign key to Slot entity
        public int CoachId { get; set; } // Foreign key to CoachInfo entity
        public DateTime Time { get; set; }
        public bool Status { get; set; }

        // Navigation properties
        public virtual Slot Slot { get; set; }
        public virtual CoachInfo CoachInfo { get; set; }
        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }
    }
}