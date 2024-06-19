namespace BookStore.BLL.DTOs
{
    public class BookUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public string? ImageUrl { get; set; }

    }
}
