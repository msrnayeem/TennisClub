using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TennisWeb.CF
{
    public class CoachInfo
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

        [MaxLength(500)] // Adjust the length as needed
        public string Description { get; set; } = "not set";

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Image { get; set; } = "not set";

        [Required]
        [DefaultValue(typeof(DateTime), "")]
        public DateTime JoinDate { get; set; } = DateTime.Now;

        [DefaultValue(typeof(DateTime), "1900-10-25")]
        public DateTime DateOfBirth { get; set; } = new DateTime(1900, 10, 25);

        public virtual User User { get; set; }
        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
