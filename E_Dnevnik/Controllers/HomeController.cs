using E_Dnevnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Dnevnik.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var email = User.Identity.Name;
                var customUser = db.Users.FirstOrDefault(u => u.Email == email);

                return RedirectToAction("ShowUser/"+customUser.Id, "Users");
            }
            else
            {
                return RedirectToAction("login", "Account");
            }
            
        }

        public ActionResult ShowUser(int id)
        {
            var model = db.Users.FirstOrDefault(u => u.Id == id);
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