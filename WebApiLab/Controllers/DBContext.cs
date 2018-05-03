using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiLab.Controllers
{
    public class NewsContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Filename=Newspage.db");
            }

        //public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        //{

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsCategories>().HasKey(x => new { x.CategoryId, x.NewsId });

            modelBuilder.Entity<AuthorsNews>().HasKey(x => new { x.AuthorId, x.NewsId });
        }


    }
}