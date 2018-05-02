using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiLab.Controllers
{
    public class News
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Intro { get; set; }
        public string Paragraf { get; set; }

        public List<Category> Category { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public News()
        {
        }

    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Route("news")]
    public class NewsController : Controller
    {
        [Route("EditNews")]
        public IActionResult EditNews(News news)
        {

            using (var client = new NewsContext())
            {

                // var result = client.News.Single(x => x.Id == news.Id);
                var result = client.News.Find(news.Id);

                result.Header = news.Header;
                result.Intro = news.Intro;
                result.Paragraf = news.Paragraf;
                result.Updated = DateTime.Now;

                client.News.Attach(result);
                var entry = client.Entry(result);
                entry.State = EntityState.Modified;

                client.SaveChanges();
            }

            var removedMessage = string.Format("Nyheten med ID {0} ändrades.", news.Id);

            return Json(new
            {
                success = true,
                Message = removedMessage
            });
        }

        [Route("RemoveNews")]
        public IActionResult RemoveNews(int? newsid)
        {

            using (var client = new NewsContext())
            {

                var result = client.News.Single(x => x.Id == newsid.Value);
                client.Remove(result);
                client.SaveChanges();

            }

            var removedMessage = string.Format("Nyheten med ID {0} togs bort", newsid);

            return Json(new
            {
                success = true,
                Message = removedMessage
            });
        }

        [Route("AddNews")]
        public IActionResult AddNews(News news)
        {
            if (news.Header == null)
            {
                return BadRequest(ModelState);
            }
            news.Created = DateTime.Now;
            news.Updated = DateTime.Now;

            using (var client = new NewsContext())
            {
                client.News.Add(news);
                client.SaveChanges();

            }

            var addedMessage = string.Format("Nyheten fick ID {0}", news.Id);

            return Json(new
            {
                success = true,
                Message = addedMessage
            });
        }

        [Route("ShowAllNews")]
        public IActionResult ShowAllNews()
        {
            List<News> news = new List<News>();

            using (var client = new NewsContext())
            {
                news = (client.News.ToList());
            }

            return Json(new
            {
                success = true,
                Message = news
            });
        }

        [Route("CountNews")]
        public IActionResult CountNews()
        {
            int count = 0;

            using (var client = new NewsContext())
            {
                count = client.News.Count();
            }


            var countMessage = String.Format("{0} number of news.", count);

            return Json(new
            {
                success = true,
                Message = countMessage
            });
        }

        [Route("SeedNews")]
        public IActionResult SeedNews()
        {

            RecreateDatabase();

            SeedTheNews();


            var seedMessage = "Nyheterna seedades...";

            return Json(new
            {
                success = true,
                Message = seedMessage
            });
        }

        public void SeedTheNews()
        {
            var news1 = new News();
            news1.Header = "Seeded News Story 1";
            news1.Intro = "Lorem ipsum dolor sit amet.";
            news1.Paragraf = "Some more text.";
            news1.Created = DateTime.Now;
            news1.Updated = DateTime.Now;


            var news2 = new News();
            news2.Header = "Seeded News Story 2";
            news2.Intro = "Lorem ipsum dolor sit amet.";
            news2.Paragraf = "Some more text.";
            news2.Created = DateTime.Now;
            news2.Updated = DateTime.Now;
            news1.Category = new List<Category>()
            {
                new Category() {Name = "Nyheter"}
            };


            var news3 = new News();
            news1.Header = "Seeded News Story 1";
            news1.Intro = "Lorem ipsum dolor sit amet.";
            news1.Paragraf = "Some more text.";
            news1.Created = DateTime.Now;
            news1.Updated = DateTime.Now;
            news1.Category = new List<Category>()
            {
                new Category() {Name = "Ekonomi"}
            };

            var news4 = new News();
            news1.Header = "Seeded News Story 1";
            news1.Intro = "Lorem ipsum dolor sit amet.";
            news1.Paragraf = "Some more text.";
            news1.Created = DateTime.Now;
            news1.Updated = DateTime.Now;
            news1.Category = new List<Category>()
            {
                new Category() {Name = "Sport"}
            };

            var news5 = new News();
            news1.Header = "Seeded News Story 1";
            news1.Intro = "Lorem ipsum dolor sit amet.";
            news1.Paragraf = "Some more text.";
            news1.Created = DateTime.Now;
            news1.Updated = DateTime.Now;
            news1.Category = new List<Category>()
            {
                new Category() {Name = "Sport"}
            };

            using (var context = new NewsContext())
            {
                context.News.AddRange(news1, news2);
                context.SaveChanges();
            }

        }

        private void RecreateDatabase()
        {
            using (var client = new NewsContext())
            {
                client.Database.EnsureDeleted();
                client.Database.EnsureCreated();
            }
        }


    }
}