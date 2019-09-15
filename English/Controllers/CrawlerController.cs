using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using English.Data;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace English.Controllers
{
    public class CrawlerController : Controller
    {
        private EnglishDbContext db = new EnglishDbContext();
        public async Task<bool> InsertAsync(int _from , int _to)
        {
            // From Web
            for (int p = _from  ; p <= _to; p++)
            {
                var url = $"http://tratu.coviet.vn/hoc-tieng-anh/cap-cau-song-ngu/vietgle-tra-tu/tat-ca/trang-{p}.html";
                var web = new HtmlWeb();
                var doc = web.Load(url);
                var sentence_nodes = doc.DocumentNode.SelectNodes("//*[@id='ctl00_ContentPlaceHolderMain_ctl00']/ul");
                var sentences = new List<Lesson>();
                foreach (var item in sentence_nodes)
                {
                    var english_node = item.FirstChild;
                    var english = english_node.InnerHtml;

                    var vietnam_node = english_node.NextSibling;
                    var vietnam = vietnam_node.InnerHtml;
                    sentences.Add(new Lesson()
                    {
                        date_created = DateTime.Now,
                        date_modified = DateTime.Now,
                        deleted = 0,
                        english_text = english,
                        keywords = "",
                        title = english,
                        vietnamese_text = vietnam,
                    });
                }
                db.lessons.AddRange(sentences);
                db.SaveChanges();
                await Task.Delay(100);
            }
            
            return true;
        }

        public async Task<JsonResult> IndexAsync()
        {
            await InsertAsync(6001, 7000);
            return Json(new { ok = 7000 });
        }
    }
}