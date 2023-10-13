using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class MatchPlayer
    {
        [Key]
        public int Id { get; set; } // Primary key

        public int MatchId { get; set; } // Foreign key for Match entity

        public int PlayerId { get; set; } // Foreign key for PlayerInfo entity

        // Navigation properties
        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }

        [ForeignKey("PlayerId")]
        public virtual PlayerInfo PlayerInfo { get; set; }
    }

}