using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Persistance;

namespace Services.OutputServices
{
    public class HtmlOutput
    {
        public static string RenderArticle(News news)
        {

            return $"<div><h2 class=\"display-3\">{news.Header}</h2>" +
                   $"<p class=\"lead\">{news.Intro}</p>" +
                   $"<em class=\"blockquote\">{news.Paragraf}</em> </div>";

        }
    }
}
