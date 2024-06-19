using BookStore.DAL.Context;
using BookStore.DAL.Entities;
using BookStore.DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.DAL.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Book> GetAllWithAuthors()
        {
            return _context.Books.Include(b => b.Author);
        }

        public async Task<IEnumerable<Book>> GetBooksByAutherAsync(int authorId)
        => await _context.Books.Where(b => b.AuthorId == authorId).ToListAsync();
        public async Task<IEnumerable<Book>> GetBooksByPriceRangeAsync(decimal minPrice, decimal maxPrice)
         => await _context.Books.Where(mp => mp.Price >= minPrice && mp.Price <= maxPrice).ToListAsync();

        public async Task<IEnumerable<Book>> SearchBooksAsync(string keyWord)
        {
            if(decimal.TryParse(keyWord, out decimal ptice))
            {
                return await _context.Books.Include(b => b.Author).Where(b => b.ISBN.Contains(keyWord) ||
                                                 b.Title.Contains(keyWord)||
                                                 b.Description.Contains(keyWord)||
                                                 b.Price == ptice||
                                                 b.Author.Name.Contains(keyWord)).ToListAsync();
            }
            else
            {
                return await _context.Books.Include(b => b.Author).Where(b => b.ISBN.Contains(keyWord) ||
                                                b.Title.Contains(keyWord) ||
                                                b.Description.Contains(keyWord) ||
                                                b.Author.Name.Contains(keyWord)).ToListAsync();
            }
        }

        public async Task<int> CountAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _context.Books.CountAsync(predicate);
        }

    }
}
