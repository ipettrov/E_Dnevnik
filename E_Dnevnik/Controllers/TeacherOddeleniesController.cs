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
    public class TeacherOddeleniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeacherOddelenies
        public ActionResult Index()
        {
            var teacherOddelenies = db.TeacherOddelenies.Include(t => t.Oddelenie).Include(t => t.Teacher);
            return View(teacherOddelenies.ToList());
        }

        // GET: TeacherOddelenies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherOddelenie teacherOddelenie = db.TeacherOddelenies.Find(id);
            if (teacherOddelenie == null)
            {
                return HttpNotFound();
            }
            return View(teacherOddelenie);
        }

        // GET: TeacherOddelenies/Create
        public ActionResult Create()
        {
            ViewBag.OddelenieId = new SelectList(db.Oddelenies, "Id", "Name");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name");
            return View();
        }

        // POST: TeacherOddelenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,OddelenieId")] TeacherOddelenie teacherOddelenie)
        {
            if (ModelState.IsValid)
            {
                db.TeacherOddelenies.Add(teacherOddelenie);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.OddelenieId = new SelectList(db.Oddelenies, "Id", "Name", teacherOddelenie.OddelenieId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", teacherOddelenie.TeacherId);
            return View(teacherOddelenie);
        }

        // GET: TeacherOddelenies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherOddelenie teacherOddelenie = db.TeacherOddelenies.Find(id);
            if (teacherOddelenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.OddelenieId = new SelectList(db.Oddelenies, "Id", "Name", teacherOddelenie.OddelenieId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", teacherOddelenie.TeacherId);
            return View(teacherOddelenie);
        }

        // POST: TeacherOddelenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,OddelenieId")] TeacherOddelenie teacherOddelenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherOddelenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OddelenieId = new SelectList(db.Oddelenies, "Id", "Name", teacherOddelenie.OddelenieId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", teacherOddelenie.TeacherId);
            return View(teacherOddelenie);
        }

        public ActionResult Delete(int TeacherId, int OddelenieId)
        {

            TempData["currnetTeacherId"] = TeacherId;
            TempData["currnetOddelenieId"] = OddelenieId;
            TeacherOddelenie teacherOddelenie = db.TeacherOddelenies.FirstOrDefault(ss => ss.TeacherId == TeacherId && ss.OddelenieId == OddelenieId);
            if (teacherOddelenie == null)
            {
                return HttpNotFound();
            }
            return View(teacherOddelenie);
        }

        // POST: TeacherSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            var currnetTeacherId = (int)TempData["currnetTeacherId"];
            var currnetOddelenieId = (int)TempData["currnetOddelenieId"];
            TeacherOddelenie teacherOddelenie = db.TeacherOddelenies.FirstOrDefault(ss => ss.TeacherId == currnetTeacherId && ss.OddelenieId == currnetOddelenieId);
            db.TeacherOddelenies.Remove(teacherOddelenie);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
