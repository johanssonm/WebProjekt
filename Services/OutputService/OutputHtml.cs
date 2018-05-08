using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Services.OutputService
{
    class OutputHtml
    {
        public string RenderCard(List<News> newsList)
        {
            var sb = new StringBuilder();

            foreach (var news in newsList)
            {
                sb.Append($"<div class=\"card \" style=\"width: 18rem; margin: 5px; float:left;\">\r\n  <img class=\"card-img-top\" src=\"...\" alt=\"{news.Id}\">");
                sb.AppendLine("<div class=\"card-body\">");
                sb.AppendLine($"<h5 class=\"card-title\">{news.Header}</h5>");
                sb.AppendLine($"<p class=\"card-text\">{news.Intro}</p>");
                sb.AppendLine($"<a href=\"/news/RenderArticle?newsid={news.Id}\" class=\"btn btn-dark\">Läs mer</a>\r\n  </div>\r\n</div>");
            }

            return sb.ToString();

        }

        public string RenderArticle(News news)
        {
                return  $"<h1>{news.Header}</h1>" +
                        $"<h3>{news.Intro}</h3>" +
                        $"<div>{news.Paragraf}</div>";

        }
    }
}
