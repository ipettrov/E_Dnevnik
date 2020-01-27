using E_Dnevnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Dnevnik.Controllers
{
    public class StudentSubjectsDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: StudentSubjectsDetails
        public ActionResult Details( int SubjectId )
        {
            var studentSubjects = db.StudentSubjects.All(ss => ss.SubjectId == SubjectId);

            return View(studentSubjects);
        }
    }
}