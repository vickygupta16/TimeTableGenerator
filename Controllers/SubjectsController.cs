using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTG3.Models;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity.Infrastructure;

namespace TTG3.Controllers
{
    public class SubjectsController : Controller
    {
        private TTG_DB_Entities db = new TTG_DB_Entities();

        // GET: Subjects
        public ActionResult Index()
        {
            if (Session["userAdmin"] != null)
            {
                ViewData["theoryCount"] = db.Subjects.Where(a => a.Subject_Code.EndsWith("T")).Count();
                ViewData["practicalCount"] = db.Subjects.Where(a => a.Subject_Code.EndsWith("P")).Count();
                return View(db.Subjects.ToList().OrderBy(a=>a.Semester_Number));
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            if (Session["userAdmin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Subject_ID,Semester_Number,Subject_Code,Subject_Name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                bool isDataExist = db.Subjects.Any(
                    o => o.Subject_Code == subject.Subject_Code
                    ||
                    o.Subject_Name == subject.Subject_Name
                    );
                if (isDataExist)
                {
                    ViewBag.ErrMsg = "Cannot Add Duplicate Values";
                    return View(subject);
                }
                else
                {
                    db.Subjects.Add(subject);
                    db.SaveChanges();
                    ViewBag.Saved = true;
                    return RedirectToAction("Index");
                }
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Subject subject = db.Subjects.Find(id);
                if (subject == null)
                {
                    return HttpNotFound();
                }
                return View(subject);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Subject_ID,Semester_Number,Subject_Code,Subject_Name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Subject subject = db.Subjects.Find(id);
                if (subject == null)
                {
                    return HttpNotFound();
                }
                return View(subject);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            try
            {
                db.Subjects.Remove(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(DbUpdateException dbexe)
            {
                ViewBag.dbexemsg = "Cannot Delete Record, Another Entity is using this Info.";
                return View(subject);
            }
            catch(Exception e)
            {
                ViewBag.ErrMsg = "Something went Wrong";
                return View(subject);
            }
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
