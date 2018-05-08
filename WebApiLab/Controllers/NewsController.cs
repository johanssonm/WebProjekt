﻿using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Services.OutputServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WebApiLab.Controllers
{
    [Route("News")] //:TODO: Döp om route. e.g. ta bort news från alla undersidor
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("EditNews")]
        public IActionResult EditNews(News news)
        {
            //var result = _unitOfWork.News.Get(news.Id);
            //result.Header = news.Header;
            //result.Intro = news.Intro;
            //result.Paragraf = news.Paragraf;
            //result.Updated = DateTime.Now;

            //_unitOfWork.News

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
        public IActionResult RemoveNews(int? newsid) //TODO: Ta bort anonym typ
        {
            var result = _unitOfWork.News.Get(newsid);
            _unitOfWork.News.Remove(result);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();

            var removedMessage = string.Format("Nyheten med ID {0} togs bort", newsid);

            return Json(new
            {
                success = true,
                Message = removedMessage
            });
        }

        [Route("AddNews")]
        public IActionResult AddNews(News news, FormHelper formhelper)
        {

            if (news.Header == null || formhelper.CategoryId == null)
            {
                return BadRequest(ModelState); // TODO: Märk upp meddelande för modelstate 

            }

            news.Created = DateTime.Now;
            news.Updated = DateTime.Now;


            var author = _unitOfWork.Author.Get(formhelper.AuthorId);
            var authornews = new AuthorsNews(news, author);

            foreach (var category in formhelper.CategoryId)
            {
                var tmpnewscategory = new NewsCategories
                {
                    NewsId = news.Id,
                    CategoryId = category
                };

                _unitOfWork.NewsCategories.Add(tmpnewscategory);
            }

            _unitOfWork.News.Add(news);
            _unitOfWork.AuthorNews.Add(authornews);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();

            var addedMessage = string.Format("Nyheten fick ID {0}", news.Id);

            return Json(new
            {
                success = true,
                Message = addedMessage
            });
        }

        [Route("JsonFeed")]
        public IActionResult JsonFeed()
        {
            var news = _unitOfWork.News.GetAll().ToList();
            _unitOfWork.Dispose();

            return Ok(news);
        }

        [Route("GetNewsCategory")]
        public List<Category> GetNewsCategory(News news)
        {

            return _unitOfWork.Category.GetAll().ToList();
        }

        [Route("RenderCard")]
        public string RenderCard(News news) //TODO: Flytta till ett interface
        {
            var sb = new StringBuilder();

            sb.Append($"<div class=\"card \" style=\"width: 18rem; margin: 5px; float:left;\">\r\n  <img class=\"card-img-top\" src=\"...\" alt=\"{news.Id}\">");
            sb.AppendLine("<div class=\"card-body\">");
            sb.AppendLine($"<h5 class=\"card-title\">{news.Header}</h5>");
            sb.AppendLine($"<p class=\"card-text\">{news.Intro}</p>");
            sb.AppendLine($"<a href=\"/news/RenderArticle?newsid={news.Id}\" class=\"btn btn-dark\">Läs mer</a>\r\n  </div>\r\n</div>");



            string html = sb.ToString();


            return html;


        }


        [Route("SingleArticle")]
        public IActionResult SingleArticle(News news)
        {
            return Content(HtmlOutput.RenderArticle(news), "text/html");
        }



        [Route("FirstPage")] //TODO: Flytta till ett interface
        public IActionResult FirstPage()
        {
            using (var client = new NewsContext())
            {



                var sb = new StringBuilder();

                foreach (var news in client.News.ToList())
                {
                    sb.Append(RenderCard(news));
                }


                string html = sb.ToString();


                return Json(new
                {
                    success = true,
                    Message = html
                });


            }
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
        public IActionResult SeedNews() //TODO: Hitta på ett bättre namn(?)
        {

            RecreateDatabase();

            SeedTheCategories();

            SeedTheAuthors();

            SeedTheNews();


            var seedMessage = "<div class=\"alert alert-success\" role=\"alert\">\r\n  Databasen initierades med nyheter, kategorier och författare... \r\n</div>";

            return Json(new
            {
                success = true,
                Message = seedMessage
            });
        }

        public void SeedTheNews() // TODO: Kanske flytta till en testdata-repository
        {
            var context = new NewsContext();

            var news1 = new News();
            news1.Header = "En fotbollsartikel";
            news1.Intro = "Lorem ipsum dolor sit amet.";
            news1.Paragraf = "Some more text.";
            news1.Created = DateTime.Now;
            news1.Updated = DateTime.Now;

            var news2 = new News();
            news2.Header = "En lokal artikel";
            news2.Intro = "Lorem ipsum dolor sit amet.";
            news2.Paragraf = "Some more text.";
            news2.Created = DateTime.Now;
            news2.Updated = DateTime.Now;

            var news3 = new News();
            news3.Header = "En krönika";
            news3.Intro = "Lorem ipsum dolor sit amet.";
            news3.Paragraf = "Some more text.";
            news3.Created = DateTime.Now;
            news3.Updated = DateTime.Now;

            var news4 = new News();
            news4.Header = "Ett reportage om Kicki Danielsson";
            news4.Intro = "Lorem ipsum dolor sit amet.";
            news4.Paragraf = "Some more text.";
            news4.Created = DateTime.Now;
            news4.Updated = DateTime.Now;

            var news5 = new News();
            news5.Header = "Ny kungabäbis";
            news5.Intro = "Lorem ipsum dolor sit amet.";
            news5.Paragraf = "Some more text.";
            news5.Created = DateTime.Now;
            news5.Updated = DateTime.Now;

            using (context)
            {
                context.AddRange(news1, news2, news3, news4, news5);

                var result1 = context.Categories.Single(x => x.Id == 3);

                var result2 = context.Categories.Single(x => x.Id == 4);

                var result3 = context.Authors.Single(x => x.Id == 1);


                var newscategory1 = new NewsCategories()
                {
                    NewsId = news1.Id,
                    CategoryId = result1.Id
                };

                var newscategory2 = new NewsCategories()
                {
                    NewsId = news1.Id,
                    CategoryId = result2.Id
                };

                var newscategory3 = new NewsCategories()
                {
                    NewsId = news2.Id,
                    CategoryId = result1.Id
                };

                var newscategory4 = new NewsCategories()
                {
                    NewsId = news3.Id,
                    CategoryId = result1.Id
                };

                var authornews = new AuthorsNews(news1, result3);

                var authornews1 = new AuthorsNews(news2, result3);

                context.AddRange(newscategory1, newscategory2, newscategory3, newscategory4, authornews, authornews1);

                context.SaveChanges();
            }

        }

        public static void SeedTheCategories()   // TODO: Kanske flytta till en testdata-behållare
        {

            var category1 = new Category("Nyheter");
            var category2 = new Category("Ekonomi");
            var category3 = new Category("Sport");
            var category4 = new Category("Lokalt");
            var category5 = new Category("Inrikes");
            var category6 = new Category("Vetenskap");
            var category7 = new Category("Världen");

            using (var context = new NewsContext())
            {
                context.AddRange(category1, category2, category3, category4, category5, category6, category7);

                context.SaveChanges();
            }

        }

        public static void SeedTheAuthors()  // TODO: Kanske flytta till en testdata-behållare
        {

            var author1 = new Author("Godzilla Hårddisksson");
            var author2 = new Author("Tord Yvel");
            var author3 = new Author("Billy Texas");

            using (var context = new NewsContext())
            {
                context.AddRange(author1, author2, author3);

                context.SaveChanges();
            }

        }

        private void RecreateDatabase()  // TODO: Kanske flytta till db-services
        {
            using (var client = new NewsContext())
            {
                client.Database.EnsureDeleted();
                client.Database.EnsureCreated();
            }
        }


    }
}