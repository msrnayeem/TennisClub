using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class CoachInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string Image { get; set; }
        public DateTime JoinDate { get; set; } // Add Join Date property
        public DateTime DateOfBirth { get; set; } // Add Date of Birth property

        // Navigation property
        public virtual User User { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}