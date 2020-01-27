using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Dnevnik.Models;

namespace E_Dnevnik.Controllers
{
    public class StudentOddeleniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentOddelenies
        public ActionResult Index()
        {
            var studentOddelenies = db.StudentOddelenies.Include(s => s.Oddelenie).Include(s => s.Student);
            return View(studentOddelenies.ToList());
        }

        // GET: StudentOddelenies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentOddelenie studentOddelenie = db.StudentOddelenies.Find(id);
            if (studentOddelenie == null)
            {
                return HttpNotFound();
            }
            return View(studentOddelenie);
        }

        // GET: StudentOddelenies/Create
        public ActionResult Create(int StudentId)
        {
            ViewBag.OddelenieId = new SelectList(db.Oddelenies, "Id", "Name");
            var student = db.Students.ToList().FirstOrDefault(t => t.Id == StudentId);
            TempData["StudentName"] = student.Name;
            TempData["StudentId"] = student.Id;
            return View();
        }

        // POST: StudentOddelenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,OddelenieId")] StudentOddelenie studentOddelenie)
        {
            if (ModelState.IsValid)
            {
                db.StudentOddelenies.Add(studentOddelenie);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.OddelenieId = new SelectList(db.Oddelenies, "Id", "Name", studentOddelenie.OddelenieId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", studentOddelenie.StudentId);
            return View(studentOddelenie);
        }

        // GET: StudentOddelenies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentOddelenie studentOddelenie = db.StudentOddelenies.Find(id);
            if (studentOddelenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.OddelenieId = new SelectList(db.Oddelenies, "Id", "Name", studentOddelenie.OddelenieId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", studentOddelenie.StudentId);
            return View(studentOddelenie);
        }

        // POST: StudentOddelenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,OddelenieId")] StudentOddelenie studentOddelenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentOddelenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OddelenieId = new SelectList(db.Oddelenies, "Id", "Name", studentOddelenie.OddelenieId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", studentOddelenie.StudentId);
            return View(studentOddelenie);
        }

        // GET: StudentOddelenies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentOddelenie studentOddelenie = db.StudentOddelenies.Find(id);
            if (studentOddelenie == null)
            {
                return HttpNotFound();
            }
            return View(studentOddelenie);
        }

        // POST: StudentOddelenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentOddelenie studentOddelenie = db.StudentOddelenies.Find(id);
            db.StudentOddelenies.Remove(studentOddelenie);
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
