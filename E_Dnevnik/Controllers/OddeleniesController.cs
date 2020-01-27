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
    public class OddeleniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Oddelenies
        public ActionResult Index()
        {
            return View(db.Oddelenies.ToList());
        }

        // GET: Oddelenies/Details/5
        public ActionResult Details(int OddelenieId)
        {
            var odd = db.Oddelenies.Find(OddelenieId);
            StudentOddeleniaDetails studentOddeleniaDetails = new StudentOddeleniaDetails();
            var studentSubjects = new List<StudentSubject>();
            var studentOddelenia = db.StudentOddelenies.ToList().FindAll(ss => ss.OddelenieId == OddelenieId);
            foreach (var student in studentOddelenia.ToList().Select(s=>s.Student))
            {
                var ss = db.StudentSubjects.FirstOrDefault(s => s.StudentId == student.Id);
                studentSubjects.Add(ss);
            }

            if (studentOddelenia == null)
            {
                return HttpNotFound();
            }
            studentOddeleniaDetails.Name = odd.Name;
            studentOddeleniaDetails.StudentOddelenies = studentOddelenia;
            studentOddeleniaDetails.StudentSubjects = studentSubjects;
            return View(studentOddeleniaDetails);
        }

        // GET: Oddelenies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Oddelenies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Oddelenie oddelenie)
        {
            if (ModelState.IsValid)
            {
                db.Oddelenies.Add(oddelenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oddelenie);
        }

        // GET: Oddelenies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oddelenie oddelenie = db.Oddelenies.Find(id);
            if (oddelenie == null)
            {
                return HttpNotFound();
            }
            return View(oddelenie);
        }

        // POST: Oddelenies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Oddelenie oddelenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oddelenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oddelenie);
        }

        // GET: Oddelenies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oddelenie oddelenie = db.Oddelenies.Find(id);
            if (oddelenie == null)
            {
                return HttpNotFound();
            }
            return View(oddelenie);
        }

        // POST: Oddelenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oddelenie oddelenie = db.Oddelenies.Find(id);
            db.Oddelenies.Remove(oddelenie);
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
