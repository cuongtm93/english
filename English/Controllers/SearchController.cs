using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using English.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace English.Controllers
{
    public class SearchController : Controller
    {
        private EnglishDbContext db = new EnglishDbContext();

        public IActionResult Index(string keyword)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (string.IsNullOrWhiteSpace(keyword))
                keyword = "";
            ViewBag.SetencesCount = db.lessons.Count();
            ViewBag.ElapsedMilliseconds = 0;
            var model = new List<Lesson>();
            if (keyword.Length > 3)
            {
                model = db.lessons.FromSql("SELECT * FROM lessons WHERE lessons.english_text LIKE @keyword AND deleted = 0", new MySqlParameter("keyword", $"%{keyword}%"))
                    .ToList<Lesson>();

                ViewBag.ElapsedMilliseconds = watch.ElapsedMilliseconds;
                ViewBag.TotalFound = model.Count;
                return View(model);
            }
            ViewBag.TotalFound = 0;
            return View(model);
        }
    }
}