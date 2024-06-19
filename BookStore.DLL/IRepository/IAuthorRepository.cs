using BookStore.DAL.Entities;

namespace BookStore.DAL.IRepository
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author> GetAuthorByName(string name);
        Task<Author> SearchAuthorsAsync(string keyWord);
    }
}
