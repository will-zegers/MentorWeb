using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MentorWeb.Models
{
    public class Skill
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int YearsExperience { get; set; }
        [ForeignKey("Profile")]
        public string ProfileID { get; set; }
        public Profile Profile { get; set; }
        public string UserName { get; set; }
    }
}