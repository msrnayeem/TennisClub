//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TennisWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class coachInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string phone { get; set; }
        public string description { get; set; }
        public int userId { get; set; }
        public string image { get; set; }
        public Nullable<System.DateTime> joinDate { get; set; }
    
        public virtual user user { get; set; }
    }
}
