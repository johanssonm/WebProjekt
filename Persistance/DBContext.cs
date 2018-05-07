using Domain;
using Microsoft.EntityFrameworkCore;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Persistance
{


    //TODO Gör ett interface av contexten och injecta interfacet i controllern. För att enkelt kunna byta ut vilken context som används.
    public class NewsContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsCategories> NewsCategories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorsNews> AuthorsNews { get; set; }

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