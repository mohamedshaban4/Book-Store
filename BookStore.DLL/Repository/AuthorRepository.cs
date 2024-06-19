using BookStore.DAL.Context;
using BookStore.DAL.Entities;
using BookStore.DAL.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Repository
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Author> GetAuthorByName(string name)
         => await _context.Authors.Where(n => n.Name == name).SingleOrDefaultAsync();

        public async Task<Author> SearchAuthorsAsync(string keyWord)
        {
            if(int.TryParse(keyWord,out  int id))
            {
                return await _context.Authors
                                            .Include(b => b.Books)
                                            .Where(a => a.Name.Contains(keyWord) ||
                                                    a.Id == id ||
                                                    a.Books.Any(b => b.Title == keyWord || b.Id == id)).SingleOrDefaultAsync();
            }
            else
            {
                return await _context.Authors
                                            .Include(b=> b.Books)
                                            .Where(a => a.Name.Contains(keyWord) ||
                                                    a.Books.Any(b => b.Title == keyWord || b.Id == id)).SingleOrDefaultAsync();
            }
        }
    }
}