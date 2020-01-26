using E_Dnevnik.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace E_Dnevnik.Controllers
{
    public class TeacherSubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeacherSubjects
        public ActionResult Index()
        {
            var teacherSubjects = db.TeacherSubjects.Include(t => t.Subject).Include(t => t.Teacher);
            return View(teacherSubjects.ToList());
        }

        // GET: TeacherSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSubject teacherSubject = db.TeacherSubjects.Find(id);
            if (teacherSubject == null)
            {
                return HttpNotFound();
            }
            return View(teacherSubject);
        }

        // GET: TeacherSubjects/Create
        public ActionResult Create(int TeacherId)
        {
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            var teacher = db.Teachers.ToList().FirstOrDefault(t => t.Id == TeacherId);
            TempData["TeacherName"] = teacher.Name;
            TempData["TeacherId"] = teacher.Id;
            return View();
        }

        // POST: TeacherSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,SubjectId")] TeacherSubject teacherSubject)
        {

            var ts = db.TeacherSubjects.ToList();
            if (ModelState.IsValid)
            {
                if (!db.TeacherSubjects.ToList().Where(t => t.SubjectId == teacherSubject.SubjectId && t.TeacherId == teacherSubject.TeacherId).Any())
                {
                    db.TeacherSubjects.Add(teacherSubject);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }

            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", teacherSubject.SubjectId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", teacherSubject.TeacherId);
            return View(teacherSubject);
        }

        // GET: TeacherSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSubject teacherSubject = db.TeacherSubjects.Find(id);
            if (teacherSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", teacherSubject.SubjectId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", teacherSubject.TeacherId);
            return View(teacherSubject);
        }

        // POST: TeacherSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,SubjectId")] TeacherSubject teacherSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", teacherSubject.SubjectId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", teacherSubject.TeacherId);
            return View(teacherSubject);
        }

        // GET: TeacherSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherSubject teacherSubject = db.TeacherSubjects.Find(id);
            if (teacherSubject == null)
            {
                return HttpNotFound();
            }
            return View(teacherSubject);
        }

        // POST: TeacherSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherSubject teacherSubject = db.TeacherSubjects.Find(id);
            db.TeacherSubjects.Remove(teacherSubject);
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
