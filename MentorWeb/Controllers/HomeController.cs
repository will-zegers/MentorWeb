using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MentorWeb.Models;
using Microsoft.AspNet.Identity;

namespace MentorWeb.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string searchUserName)
        {
            // tried implementing with a join, didn't know how to address anonymous types
            /*
            var userProfileJoin =
                from u in db.Users
                join p in db.Profiles on u.Id equals p.ID
                select new { userName = u.UserName, firstName = p.FirstName, lastName = p.LastName, bio = p.Bio, gender = p.Gender };
            */

            var user = from p in db.Users select p;
            List<string> ids = new List<string>();
            List<Profile> profiles = new List<Profile>();
            if (!String.IsNullOrEmpty(searchUserName))
            {
                user = user.Where(s => s.UserName.Contains(searchUserName));
                foreach (var item in user)
                {
                    string id = item.Id;
                    ids.Add(id);
                }
            }
            else if (searchUserName == "")
            {
                foreach (var item in user)
                {
                    string id = item.Id;
                    ids.Add(id);
                }
            }

            foreach (var idItem in ids)
            {
                foreach (var profileItem in db.Profiles)
                {
                    if (profileItem.ID == idItem)
                        profiles.Add(profileItem);
                }
            }
            return View(profiles);
        }

        // match function
        public ActionResult Match(string searchSkill, int min = 0)
        {
            var skillList = new List<string>();
            var profile = from p in db.Profiles select p;
            List<Skill> users = new List<Skill>();

            // logic for skills drop-down menu
            var descList = from s in db.Skills orderby s.Description select s.Description;
            skillList.AddRange(descList.Distinct());
            ViewBag.searchSkill = new SelectList(skillList);

            /*
            var existingRelations = from e in db.Relationships select e.mentorID;
            List<string> existingRelationsList = new List<string>();
            existingRelationsList = existingRelations.ToList();
             */

            foreach (var item in db.Skills)
            {
                if (item.UserName != this.User.Identity.GetUserName())
                {
                    System.Diagnostics.Debug.WriteLine("test: " + searchSkill);
                    // no skill, no minimum experience
                    if (searchSkill == "" && min == 0)
                        users.Add(item);
                    // no skill, minimum experience
                    else if (searchSkill == "" && item.YearsExperience >= min)
                        users.Add(item);
                    // search skill, mininum experience
                    else if (searchSkill == item.Description && item.YearsExperience >= min)
                        users.Add(item);
                    // search skill, no mininum experience
                    else if (searchSkill == item.Description && min == 0)
                        users.Add(item);
                }
            }
            return View(users);
        }

        public ActionResult AddRelationship(string id)
        {
            Relationship r = new Relationship();
            r.menteeID = this.User.Identity.GetUserId();
            r.mentorID = id;
            r.accepted = false;
            var check = from c in db.Relationships where c.menteeID == r.menteeID && c.mentorID == r.mentorID select c;
            if (ModelState.IsValid && check.Count() == 0)
            {
                db.Relationships.Add(r);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Relationship");
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}