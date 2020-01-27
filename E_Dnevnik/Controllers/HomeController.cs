using E_Dnevnik.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Dnevnik.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index(int TeacherId = 0)
        {
            E_DnevnikModel model = new E_DnevnikModel();
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                
                string userEmail = System.Web.HttpContext.Current.User.Identity.Name;
                var context = new IdentityDbContext();
                var user = context.Users.ToList().FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    var teacher = db.Teachers.ToList().FirstOrDefault(t => t.Email == user.Email);
                    if (teacher == null)
                    {

                        model.IsTeacher = false;
                        var student = db.Students.ToList().FirstOrDefault(s => s.Email == user.Email);
                        model.Student = student;
                        var studentSubjectsForStudent = db.StudentSubjects.ToList().FindAll(s => s.StudentId == student.Id);
                        model.StudentSubjects = studentSubjectsForStudent;
                        int counter = 0;
                        double suma = 0;
                        foreach (var ss in studentSubjectsForStudent)
                        {
                            if (ss.SecondGrade != 0) counter++;
                            suma += ss.SecondGrade;
                        }
                        model.Prosek = ((double)suma / counter);
                    }
                    else
                    {
                        model.IsTeacher = true;
                        model.Teacher = teacher;
                        var teacherOddelenia = db.TeacherOddelenies.ToList().FindAll(s => s.TeacherId == teacher.Id).Select(s => s.Oddelenie).ToList();
                        model.Oddelenia = teacherOddelenia;
                        var teacherSubjects = db.TeacherSubjects.ToList().FindAll(s => s.TeacherId == teacher.Id).Select(s => s.Subject).ToList();
                        model.Subjects = teacherSubjects;
                        Dictionary<Subject, List<Student>> subjectStudentDict = new Dictionary<Subject, List<Student>>();
                        foreach (var s in teacherSubjects)
                        {
                            var students = db.StudentSubjects.ToList().FindAll(ss => ss.Subject.Id == s.Id).Select(ss => ss.Student).ToList();
                            subjectStudentDict.Add(s, students);
                        }
                        model.SubjectStudentsDictionary = subjectStudentDict;
                    }
                }
                

            }

            return View(model); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}