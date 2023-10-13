using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class PlayerMatch
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PlayerInfo")]
        public int PlayerId { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }

        public virtual PlayerInfo PlayerInfo { get; set; }
        public virtual Match Match { get; set; }
    }
}