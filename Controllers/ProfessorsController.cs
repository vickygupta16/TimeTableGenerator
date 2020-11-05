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
    public class ProfessorsController : Controller
    {
        private TTG_DB_Entities db = new TTG_DB_Entities();

        // GET: Professors
        public ActionResult Index()
        {
            if (Session["userAdmin"] != null)
            {
                ViewData["internalFaculty"] = db.Professors.Where(a => a.Visiting_Faculty == "No").Count();
                ViewData["externalFaculty"] = db.Professors.Where(a => a.Visiting_Faculty == "Yes").Count();
                return View(db.Professors.ToList().OrderBy(a=>a.Professor_Name));
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Professors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: Professors/Create
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

        // POST: Professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Professor_ID,Professor_Name,Email,Contact,Gender,Age,Visiting_Faculty,Start_Hour,End_Hour")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                bool isDataExist = db.Professors.Any(
                    o => o.Email == professor.Email
                    );
                if (isDataExist)
                {
                    ViewBag.ErrMsg = "Cannot Add Duplicate Values";
                    return View(professor);
                }
                else
                {
                    db.Professors.Add(professor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(professor);
        }

        // GET: Professors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Professor professor = db.Professors.Find(id);
                if (professor == null)
                {
                    return HttpNotFound();
                }
                return View(professor);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Professor_ID,Professor_Name,Email,Contact,Gender,Age,Visiting_Faculty,Start_Hour,End_Hour")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                bool isDataExist = db.Professors.Any(
                    o => o.Email == professor.Email
                    );
                if (isDataExist)
                {
                    ViewBag.ErrMsg = "Cannot Add Duplicate Values";
                    return View(professor);
                }
                else
                {
                    db.Entry(professor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(professor);
        }

        // GET: Professors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Professor professor = db.Professors.Find(id);
                if (professor == null)
                {
                    return HttpNotFound();
                }
                return View(professor);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professor professor = db.Professors.Find(id);
            try
            {
                db.Professors.Remove(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbexe)
            {
                ViewBag.dbexemsg = "Cannot Delete Record.";
                return View(professor);
            }
            catch (Exception e)
            {
                ViewBag.ErrMsg = "Something went Wrong";
                return View(professor);
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
