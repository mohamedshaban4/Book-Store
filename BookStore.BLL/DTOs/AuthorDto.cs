using BookStore.DAL.Entities;

namespace BookStore.BLL.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BooksCount { get; set; }
        public ICollection<Book> Books { get; set; }

    }
}
