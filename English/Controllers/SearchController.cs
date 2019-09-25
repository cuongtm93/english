using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
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

        [HttpGet]
        public IActionResult Index(string keyword)
        {
            ObjectCache cache = MemoryCache.Default;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (string.IsNullOrWhiteSpace(keyword))
                keyword = "";

            ViewBag.SetencesCount = db.lessons.Count();
            var model = new List<Lesson>();
            if (keyword.Length > 3)
            {
                ViewBag.SetencesCount = db.lessons.Count();
                var cached = cache.Get(keyword);
                if (cached != null)
                {
                    model = cached as List<Lesson>;
                }
                else
                {
                    model = db.lessons.FromSql("SELECT * FROM lessons WHERE lessons.english_text LIKE @keyword AND deleted = 0", new MySqlParameter("keyword", $"%{keyword}%"))
                   .ToList<Lesson>();
                    CacheItemPolicy policy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTime.Now + TimeSpan.FromDays(1)
                    };

                    cache.Set(keyword, model, policy);
                }


                ViewBag.ElapsedMilliseconds = watch.ElapsedMilliseconds;
                ViewBag.TotalFound = model.Count;
                return View(model);
            }
            ViewBag.TotalFound = 0;
            return View(model);
        }
    }
}