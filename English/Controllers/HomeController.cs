using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using English.Models;

namespace English.Controllers
{
    public class HomeController : Controller
    {
        private EnglishDbContext db = new EnglishDbContext();
        public IActionResult Index(int? page = 1)
        {
            int numberOfObjectsPerPage = 10;
            ViewBag.SetencesCount = db.lessons.Count();

            var model = db.lessons
                .Where(r => r.deleted == 0)
                .OrderByDescending(r => r.viewed)
                .Skip(numberOfObjectsPerPage * (page.Value - 1))
                .Take(numberOfObjectsPerPage)
                .ToList();

            return View(model);
        }

        public IActionResult Learn(int id)
        {
            var model = db.lessons.Find(id);
            model.viewed += 1;
            db.SaveChanges();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Data.Lesson model)
        {
            model.date_created = DateTime.Now;
            model.date_modified = DateTime.Now;
            model.deleted = 0;
            model.keywords = "";
            db.lessons.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = db.lessons.Find(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Data.Lesson model)
        {
            model.date_modified = DateTime.Now;
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = db.lessons.Find(id);
            model.deleted = 1;
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { page = 1 });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
