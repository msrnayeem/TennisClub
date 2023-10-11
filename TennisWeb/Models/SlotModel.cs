using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisWeb.Models
{
    public class SlotModel
    {

        [Required(ErrorMessage = "Required")]
        [StringLength(10, ErrorMessage = "Name must not exceed 10")]
        public string name { get; set; }

        [Required(ErrorMessage = "Start Time Required")]
        public string start { get; set; }

        [Required(ErrorMessage = "End Time Required")]
        public string end { get; set; }
    }
}