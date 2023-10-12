using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class MatchPlayer
    {
        public int Id { get; set; } // Primary key

        public int MatchId { get; set; }
        public int PlayerId { get; set; }

        public virtual Match Match { get; set; }
        public virtual PlayerInfo Player { get; set; }
    }
}