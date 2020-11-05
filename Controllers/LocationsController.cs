using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTG3.Models;

namespace TTG3.Controllers
{
    public class LocationsController : Controller
    {
        private TTG_DB_Entities db = new TTG_DB_Entities();

        // GET: Locations
        public ActionResult Index()
        {
            if (Session["userAdmin"] != null)
            {
                return View(db.Locations.ToList().OrderBy(a=>a.Semester_Number));
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
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

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Location_ID,Room,Semester_Number,Subject_Type")] Location location)
        {
            if (ModelState.IsValid)
            {
                bool isDataExist = db.Locations.Any(
                    o => o.Room == location.Room
                    &&
                    o.Semester_Number == location.Semester_Number
                    &&
                    o.Subject_Type == location.Subject_Type
                    );
                if (isDataExist)
                {
                    ViewBag.ErrMsg = "Cannot Add Duplicate Values";
                    return View(location);
                }
                else
                {
                    db.Locations.Add(location);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Location location = db.Locations.Find(id);
                if (location == null)
                {
                    return HttpNotFound();
                }
                return View(location);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Location_ID,Room,Semester_Number,Subject_Type")] Location location)
        {
            if (ModelState.IsValid)
            {
                bool isDataExist = db.Locations.Any(
                    o => o.Room == location.Room
                    &&
                    o.Semester_Number == location.Semester_Number
                    &&
                    o.Subject_Type == location.Subject_Type
                    );
                if (isDataExist)
                {
                    ViewBag.ErrMsg = "Cannot Add Duplicate Values";
                    return View(location);
                }
                else
                {
                    db.Entry(location).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Location location = db.Locations.Find(id);
                if (location == null)
                {
                    return HttpNotFound();
                }
                return View(location);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
