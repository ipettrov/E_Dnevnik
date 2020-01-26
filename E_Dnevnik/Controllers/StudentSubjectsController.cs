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
    public class StudentSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentSubjects
        public ActionResult Index()
        {
            var studentSubjects = db.StudentSubjects.Include(s => s.Student).Include(s => s.Subject);
            return View(studentSubjects.ToList());
        }

        // GET: StudentSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            return View(studentSubject);
        }

        // GET: StudentSubjects/Create
        public ActionResult Create(int TeacherId)
        {
            var teacher = db.Teachers.Find(TeacherId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name");
            ViewBag.SubjectId = new SelectList(db.TeacherSubjects.ToList().FindAll(s => s.TeacherId == teacher.Id).Select(s => s.Subject).ToList(), "Id", "Name");
            return View();
        }

   
        // POST: StudentSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,SubjectId,FirstGrade,SecondGrade")] StudentSubject studentSubject)
        {
            var modelSend = studentSubject;
            if (ModelState.IsValid)
            {
                var ss = db.StudentSubjects.FirstOrDefault(s => s.StudentId == studentSubject.StudentId && s.SubjectId == studentSubject.SubjectId);
                db.StudentSubjects.Remove(ss);
                db.SaveChanges();
                if (ss.FirstGrade == 0)
                {
                    
                    db.StudentSubjects.Add(studentSubject);
                    db.SaveChanges();
                }
                else
                {
                    ss.SecondGrade = studentSubject.FirstGrade;
                    db.StudentSubjects.Add(ss);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index","Home");
            }

            ViewBag.StudentId = new SelectList(db.Teachers, "Id", "Name", studentSubject.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", studentSubject.SubjectId);
            return View(studentSubject);
        }

        // GET: StudentSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Teachers, "Id", "Name", studentSubject.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", studentSubject.SubjectId);
            return View(studentSubject);
        }

        // POST: StudentSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,SubjectId,FirstGrade,SecondGrade")] StudentSubject studentSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Teachers, "Id", "Name", studentSubject.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", studentSubject.SubjectId);
            return View(studentSubject);
        }

        // GET: StudentSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            if (studentSubject == null)
            {
                return HttpNotFound();
            }
            return View(studentSubject);
        }

        // POST: StudentSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentSubject studentSubject = db.StudentSubjects.Find(id);
            db.StudentSubjects.Remove(studentSubject);
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
