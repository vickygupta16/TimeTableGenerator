using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTG3.Models;

namespace TTG3.Controllers
{
    public class SessionsController : Controller
    {
        private TTG_DB_Entities db = new TTG_DB_Entities();

        // GET: Sessions
        public ActionResult Index()
        {
            if (Session["userAdmin"] != null)
            {
                ViewData["s1subCount"] = db.Sessions.Where(a => a.Semester_Number == 1).Count();
                ViewData["s2subCount"] = db.Sessions.Where(a => a.Semester_Number == 2).Count();
                ViewData["s3subCount"] = db.Sessions.Where(a => a.Semester_Number == 3).Count();
                ViewData["s4subCount"] = db.Sessions.Where(a => a.Semester_Number == 4).Count();
                ViewData["s5subCount"] = db.Sessions.Where(a => a.Semester_Number == 5).Count();
                var sessions = db.Sessions.OrderBy(s => s.Semester_Number).Include(s => s.Professor).Include(s => s.Subject);
                return View(sessions.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Sessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: Sessions/Create
        public ActionResult Create()
        {
            //var resultant = from a in db.Subjects
            //                join b in db.Sessions
            //                on a.Subject_ID equals b.Subject_ID
            //                where a.Subject_ID == b.Subject_ID
            //                select a;
            //var resultant = from a in db.Subjects
            //                where a.Subject_ID != (from b in db.Subjects
            //                                       select b.Subject_ID)
            //                select a;
            if (Session["userAdmin"] != null)
            {
                var q1 = db.Subjects.OrderBy(p => p.Subject_Name).Where(p => db.Sessions.All(p2 => p2.Subject_ID != p.Subject_ID));
                ViewBag.Professor_ID = new SelectList(db.Professors, "Professor_ID", "Professor_Name");
                //ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Subject_Name");
                ViewBag.Subject_ID = new SelectList(q1, "Subject_ID", "Subject_Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Session_ID,Semester_Number,Subject_ID,Professor_ID,Subject_Type,Sessions_Per_Week,Priority_Level")] Session session)
        {
            if (ModelState.IsValid)
            {
                bool isDataExist = db.Sessions.Any(
                    o => o.Semester_Number == session.Semester_Number
                    &&
                    o.Subject_ID == session.Subject_ID
                    &&
                    o.Professor_ID == session.Professor_ID
                    &&
                    o.Subject_Type == session.Subject_Type
                    );
                if (isDataExist)
                {
                    ViewBag.ErrMsg = "Cannot Add Duplicate Values";
                    return View(session);
                }
                else
                {
                    db.Sessions.Add(session);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var q1 = db.Subjects.OrderBy(p => p.Subject_Name).Where(p => db.Sessions.All(p2 => p2.Subject_ID != p.Subject_ID));
            ViewBag.Professor_ID = new SelectList(db.Professors, "Professor_ID", "Professor_Name", session.Professor_ID);
            //ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Subject_Name", session.Subject_ID);
            ViewBag.Subject_ID = new SelectList(q1, "Subject_ID", "Subject_Name");
            return View(session);
        }

        // GET: Sessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Session session = db.Sessions.Find(id);
                if (session == null)
                {
                    return HttpNotFound();
                }
                //var q1 = db.Subjects.OrderBy(p => p.Subject_Name).Where(p => db.Sessions.All(p2 => p2.Subject_ID == p.Subject_ID));
                ViewBag.Professor_ID = new SelectList(db.Professors, "Professor_ID", "Professor_Name", session.Professor_ID);
                ViewBag.Subject_ID = new SelectList(db.Subjects.Where(p=>p.Subject_ID==session.Subject_ID), "Subject_ID", "Subject_Name", session.Subject_ID);
                //ViewBag.Subject_ID = new SelectList(q, "Subject_ID", "Subject_Name");
                return View(session);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Session_ID,Semester_Number,Subject_ID,Professor_ID,Subject_Type,Sessions_Per_Week,Priority_Level")] Session session)
        {
            if (ModelState.IsValid)
            {
                //bool isDataExist = db.Sessions.Any(
                //    o => o.Semester_Number == session.Semester_Number
                //    &&
                //    o.Subject_ID == session.Subject_ID
                //    &&
                //    o.Professor_ID == session.Professor_ID
                //    &&
                //    o.Subject_Type == session.Subject_Type
                //    );
                //if (isDataExist)
                //{
                //    ViewBag.ErrMsg = "Cannot Add Duplicate Values";
                //    return View(session);
                //}
                //else
                //{
                try
                {
                    db.Entry(session).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(DbUpdateException dbue)
                {
                    ViewData["dbueem"] = dbue.Message;
                }
                catch(Exception e)
                {
                    ViewData["em"] = "Something Went Wrong";
                }
            //    }
            }
            ViewBag.Professor_ID = new SelectList(db.Professors, "Professor_ID", "Professor_Name", session.Professor_ID);
            ViewBag.Subject_ID = new SelectList(db.Subjects.Where(p => p.Subject_ID == session.Subject_ID), "Subject_ID", "Subject_Name", session.Subject_ID);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Session session = db.Sessions.Find(id);
                if (session == null)
                {
                    return HttpNotFound();
                }
                return View(session);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
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
    }
}
