using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApiLab.Controllers
{
    public class News
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Intro { get; set; }
        public string Paragraf { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<NewsCategories> NewsCategorieses { get; set; }
        public News()
        {
        }

    }

    public class NewsCategories
    {
        public int NewsId { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public News News { get; set; }

    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<NewsCategories> NewsCategories { get; set; }
    }

    public class CategorySerializer
    {
        public string[] CategoryName { get; set; }
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
        public IActionResult AddNews(News news, CategorySerializer categories)
        {

            if (news.Header == null || categories.CategoryName == null)
            {
                return BadRequest(ModelState); // TODO: Märk upp meddelande för modelstate 
                                               // TODO: Validera att rubrik och kategori är unik(?)
            }

            using (var context = new NewsContext())
            {
                news.Created = DateTime.Now;
                news.Updated = DateTime.Now;
                context.Add(news);
                context.SaveChanges();


                for (int i = 0; i < categories.CategoryName.Length; i++)
                {
                    var tmpcategory = new Category();

                    tmpcategory.Name = categories.CategoryName[i];

                    var tmpnewscategory = new NewsCategories()
                    {
                        News = news,
                        Category = tmpcategory,
                    };

                        context.AddRange(tmpcategory, tmpnewscategory);
                        context.SaveChanges();
                    

                }

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
            
            var category1 = new Category();

            category1.Name = "Sport";


            var newscategory1 = new NewsCategories()
            {
                News = news1,
                Category = category1,
            };


            using (var context = new NewsContext())
            {
                context.AddRange(news1,category1,newscategory1);
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