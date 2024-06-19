using BookStore.DAL.Context;
using BookStore.DAL.IRepository;
using BookStore.DAL.Repository;

namespace BookStore.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IBookRepository _books;
        private IAuthorRepository _authors;
       

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBookRepository Books => _books ??= new BookRepository(_context);
        public IAuthorRepository Authors => _authors ??= new AuthorRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
         => _context.Dispose();
    }
}
