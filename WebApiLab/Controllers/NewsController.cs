using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace WebApiLab.Controllers
{
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

        [Route("RenderArticle")]
        public string RenderArticle(int newsid)
        {
            using (var client = new NewsContext())
            {
                var article = client.News.Single(x => x.Id == newsid);

                var sb = new StringBuilder();


                sb.AppendLine($"<h1>{article.Header}</h1>");
                sb.AppendLine($"<h3>{article.Intro}</h3>");
                sb.AppendLine($"<div>{article.Paragraf}</div>");

                return sb.ToString();
            }


        }

        [Route("FirstPage")]
        public IActionResult RenderArticle()
        {
            using (var client = new NewsContext())
            {
                var sb = new StringBuilder();

                sb.Append(RenderHeader());
                sb.AppendLine();


                string html = sb.ToString();

                return Content(html, "text/html");
            }


        }

        public string RenderHeader()
        {
            var sb = new StringBuilder();

            sb.Append(
                "<html><head><title>WebProjekt</title><link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css\" integrity=\"sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4\" crossorigin=\"anonymous\"><link href=\"../public/assets/css/simple-sidebar.css\" rel=\"stylesheet\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head><body><div id=\"wrapper\"><div id=\"sidebar-wrapper\"><ul class=\"sidebar-nav\"><li class=\"sidebar-brand\"><a href=\"#\">WebProjekt &#9729;</a></li><li><a href=\"index.html\">Startsida</a></li><li><a href=\"create.html\">Skapa artikel</a></li><li><a href=\"edit.html\">Redigera artikel</a></li><li><a href=\"article.html\">Artikelvy</a></li><li><a href=\"archive.html\">Arkiv</a></li></ul></div><div id=\"page-content-wrapper\"><div class=\"container-fluid\">");
            return sb.ToString();
        }

        public string RenderFooter()
        {
            var sb = new StringBuilder();

            sb.Append("</div></div><script src=\"../public/assets/vendor/jquery/jquery.min.js\"></script><script src=\"../public/assets/vendor/bootstrap/js/bootstrap.bundle.min.js\"></script><script src=\"../public/assets/js/news.js\"></script><script>$(\"#menu-toggle\").click(function(e) {e.preventDefault();$(\"#wrapper\").toggleClass(\"toggled\");});</script></body></html>");
            return sb.ToString();
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

            var news2 = new News();
            news2.Header = "En fotbollsartikel";
            news2.Intro = "Lorem ipsum dolor sit amet.";
            news2.Paragraf = "Some more text.";
            news2.Created = DateTime.Now;
            news2.Updated = DateTime.Now;

            var news3 = new News();
            news3.Header = "En fotbollsartikel";
            news3.Intro = "Lorem ipsum dolor sit amet.";
            news3.Paragraf = "Some more text.";
            news3.Created = DateTime.Now;
            news3.Updated = DateTime.Now;

            var news4 = new News();
            news4.Header = "En fotbollsartikel";
            news4.Intro = "Lorem ipsum dolor sit amet.";
            news4.Paragraf = "Some more text.";
            news4.Created = DateTime.Now;
            news4.Updated = DateTime.Now;

            var news5 = new News();
            news5.Header = "En fotbollsartikel";
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