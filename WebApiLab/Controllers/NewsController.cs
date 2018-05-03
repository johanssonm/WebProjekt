using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace WebApiLab.Controllers
{
    public class News
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Intro { get; set; }
        public string Paragraf { get; set; }
        public string FeaturedImage { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<NewsCategories> NewsCategorieses { get; set; }
        public List<AuthorsNews> Authornews { get; set; }

    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<NewsCategories> NewsCategories { get; set; }

        public Category()
        {
            
        }

        public Category(string name)
        {
            Name = name;
        }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }

        public List<AuthorsNews> Authornews { get; set; }

        public Author()
        {
            
        }

        public Author(string name)
        {
            Name = name;
            Avatar = $"assets/img/avatar.png";
        }

    }

    public class AuthorsNews
    {
        public int AuthorId { get; set; }
        public int NewsId { get; set; }
        public Author Author { get; set; }
        public News News { get; set; }

        public AuthorsNews(News news, Author author)
        {
            News = news;
            Author = author;
        }
    }

    public class NewsCategories
    {
        // public int Id { get; set; }
        public int NewsId { get; set; }
        public int CategoryId { get; set; }

        // public Category Category { get; set; }

        // public News News { get; set; }

    }

    public class FormHelper
    {
        public int[] CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string Image { get; set; }

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
        public IActionResult AddNews(News news, FormHelper formhelper)
        {

            if (news.Header == null || formhelper.CategoryId == null)
            {
                return BadRequest(ModelState); // TODO: Märk upp meddelande för modelstate 
                                               // TODO: Validera att rubrik och kategori är unik(?)
            }

            using (var context = new NewsContext())
            {

                news.Created = DateTime.Now;
                news.Updated = DateTime.Now;

                context.Add(news);

                var author = context.Authors.Single(x => x.Id == formhelper.AuthorId);

                var authornews = new AuthorsNews(news, author);

                context.Add(authornews);

                context.SaveChanges();


                for (int i = 0; i < formhelper.CategoryId.Length; i++)
                {
                    
                    var tmpnewscategory = new NewsCategories()
                    {
                        NewsId = news.Id,
                        CategoryId = formhelper.CategoryId[i]
                    };

                    context.Add(tmpnewscategory);
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

        [Route("JsonFeed")]
        public IActionResult JsonFeed()
        {
            List<News> news = new List<News>();

            using (var client = new NewsContext())
            {
                news = (client.News.ToList());
            }

            return Ok(news);
        }

        [Route("NewsTable")]
        public IActionResult NewsTable()
        {
            List<News> news = new List<News>();

            using (var client = new NewsContext())
            {
                news = (client.News.ToList());
            }



            var html = ConvertDataTableToHTML(ConvertNewsToDataTable(news));

            return Content(html, "text/html");
        }

        public DataTable ConvertNewsToDataTable(List<News> list)
        {
            var newsList = new List<News>();

            using (DataTable dt = new DataTable())
            {
                dt.TableName = "Nyheter";
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Rubrik", typeof(string));
                dt.Columns.Add("Intro", typeof(string));
                dt.Columns.Add("Paragraf", typeof(string));
                //dt.Columns.Add("Kategori", typeof(string));
                dt.Columns.Add("Skapad", typeof(DateTime));
                dt.Columns.Add("Ändrad", typeof(DateTime));

                foreach (var news in list) // TODO: Convertera antal kategorier till kategorinamn(?)
                {
                    dt.Rows.Add(news.Id, news.Header, news.Intro, news.Paragraf, news.Created, news.Updated);
                }

                return dt;
            }
        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
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

            SeedTheCategories();

            SeedTheAuthors();

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
            var context = new NewsContext();

            var news1 = new News();
            news1.Header = "En fotbollsartikel";
            news1.Intro = "Lorem ipsum dolor sit amet.";
            news1.Paragraf = "Some more text.";
            news1.Created = DateTime.Now;
            news1.Updated = DateTime.Now;

            using (context)
            {
                context.Add(news1);

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

                var authornews = new AuthorsNews(news1, result3);

                context.AddRange(newscategory1,newscategory2, authornews);

                context.SaveChanges();
            }

        }

        public static void SeedTheCategories()
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

        public static void SeedTheAuthors()
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