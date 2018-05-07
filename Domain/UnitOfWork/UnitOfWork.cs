using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            Author = new AuthorRepository(_context);
            News = new NewsRepository(_context);
            AuthorNews = new AuthorNewsRepository(_context);
            Category = new CategoryRepository(_context);
            NewsCategories = new NewsCategoriesRepository(_context);
        }


        public INewsRepository News { get; }
        public IAuthorRepository Author { get; }
        public IAuthorNewsRepository AuthorNews { get; }
        public ICategoryRepository Category { get; }
        public INewsCategoriesRepository NewsCategories { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}