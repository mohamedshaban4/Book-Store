using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookStore.BLL.DTOs
{
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public int Id { get; set; }

        public IFormFile Image { get; set; }

    }
}
