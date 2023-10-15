using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Required]
        [MaxLength(255)] // Adjust the length as needed
        public string Name { get; set; } = "not set";

        [MaxLength(255)] // Adjust the length as needed
        public string Address { get; set; } = "not set";

        [MaxLength(20)] // Adjust the length as needed
        public string Phone { get; set; } = "not set";

        [Required]
        [MaxLength(50)] // Adjust the length as needed
        public string PlayingPosition { get; set; } = "not set";

        [Required]
        [Index(IsUnique = true)]
        public int UserId { get; set; }

        public string Image { get; set; } = "not set";

        [Required]
        [DefaultValue(typeof(DateTime), "")]
        public DateTime JoinDate { get; set; } = DateTime.Now;

        [DefaultValue(typeof(DateTime), "1900-10-25")]
        public DateTime? DateOfBirth { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; } = new List<MatchPlayer>();
    }
}