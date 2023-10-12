using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class Slot
    {
        public int Id { get; set; } // Primary key, auto-incremented by default
        public string Name { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}