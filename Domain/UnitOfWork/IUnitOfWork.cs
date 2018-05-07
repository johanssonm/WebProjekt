using System;

namespace Domain
{
    public interface IUnitOfWork : IDisposable
    {
        INewsRepository News { get; }
        IAuthorRepository Author { get; }
        int Complete();
    }
}