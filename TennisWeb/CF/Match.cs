using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Slot")]
        public int SlotId { get; set; }

        [ForeignKey("CoachInfo")]
        public int CoachId { get; set; }

        public DateTime Time { get; set; }
        public bool Status { get; set; }

        public virtual Slot Slot { get; set; }
        public virtual CoachInfo CoachInfo { get; set; }
        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; } = new List<MatchPlayer>();
    }
}