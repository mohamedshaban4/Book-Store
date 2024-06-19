using BookStore.DAL.IRepository;

namespace BookStore.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
        Task<int> CompleteAsync ();

    }
}
