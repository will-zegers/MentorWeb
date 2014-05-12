using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MentorWeb.Models;
using Microsoft.AspNet.Identity;

namespace MentorWeb.Controllers
{
    public class RelationshipController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Relationship/
        public ActionResult Index()
        {
            return View(db.Relationships.ToList());
        }

        // GET: /Relationship/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relationship relationship = db.Relationships.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        // GET: /Relationship/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Relationship/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="rID,mentorID,menteeID,accepted")] Relationship relationship)
        {
            if (ModelState.IsValid)
            {
                db.Relationships.Add(relationship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(relationship);
        }

        // GET: /Relationship/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relationship relationship = db.Relationships.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        // POST: /Relationship/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="rID,mentorID,menteeID,accepted")] Relationship relationship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relationship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(relationship);
        }

        // GET: /Relationship/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relationship relationship = db.Relationships.Find(id);
            if (relationship == null)
            {
                return HttpNotFound();
            }
            return View(relationship);
        }

        // POST: /Relationship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relationship relationship = db.Relationships.Find(id);
            db.Relationships.Remove(relationship);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Mentees()
        {
            // wasn't working -- not recognized as a list / can't iterate
            /*
            string currentID = this.User.Identity.GetUserId();
            var menteelist = from m in db.Relationships select m; // select mentees
            List<Profile> menteelist2 = new List<Profile>();
            foreach (var item in menteelist)
            {
                int i = 0; // menteelist2.Add(profile);
            }
            return View(menteelist2);
             */

            string currentID = this.User.Identity.GetUserId();
            var user = from p in db.Relationships select p;
            List<string> ids = new List<string>();
            List<Profile> profiles = new List<Profile>();

            user = user.Where(s => s.mentorID == currentID);
            foreach (var item in user)
            {
                string id = item.menteeID;
                ids.Add(id);
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
    }
}
