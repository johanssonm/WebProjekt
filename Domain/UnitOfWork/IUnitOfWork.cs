using System;

namespace Domain
{
    public interface IUnitOfWork : IDisposable
    {
        INewsRepository News { get; }
        IAuthorRepository Author { get; }
        IAuthorNewsRepository AuthorNews { get; }
        ICategoryRepository Category { get; }
        INewsCategoriesRepository NewsCategories { get; }
        int Complete();
    }
}