using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TennisWeb.Models;

namespace TennisWeb.CF
{
    public class PlayerInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PlayingPosition { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public string Image { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; } = new List<MatchPlayer>();
    }
}