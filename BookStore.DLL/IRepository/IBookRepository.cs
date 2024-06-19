using BookStore.DAL.Entities;
using System.Linq.Expressions;

namespace BookStore.DAL.IRepository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task <IEnumerable<Book>> GetBooksByAutherAsync(int authorId);
        IQueryable<Book> GetAllWithAuthors();
        Task<int> CountAsync(Expression<Func<Book, bool>> predicate);
        Task<IEnumerable<Book>> SearchBooksAsync(string keyWord);
        Task<IEnumerable<Book>> GetBooksByPriceRangeAsync(decimal minPrice, decimal maxPrice);

    }
}
