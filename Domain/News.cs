using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class News
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Intro { get; set; }
        public string Paragraf { get; set; }
        public string FeaturedImage { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Created { get; set; } 
        public DateTime Updated { get; set; }
        public List<NewsCategories> NewsCategorieses { get; set; }
        public List<AuthorsNews> Authornews { get; set; }

    }
}