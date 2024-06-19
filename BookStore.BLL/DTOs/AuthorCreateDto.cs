using BookStore.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BLL.DTOs
{
    public class AuthorCreateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public int BooksCount { get; set; }

    }
}
