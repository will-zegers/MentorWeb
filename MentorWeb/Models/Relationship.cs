using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorWeb.Models
{
    public class Relationship
    {
        [Key]
        public int rID { get; set; }
        // [ForeignKey("Users")]
        public string mentorID { get; set; }
        // [ForeignKey("Users")]
        public string menteeID { get; set; }
        public bool accepted { get; set; }
    }
}