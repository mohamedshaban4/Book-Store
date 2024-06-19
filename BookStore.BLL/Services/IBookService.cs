using BookStore.BLL.DTOs;
using BookStore.DAL.Entities;

namespace BookStore.BLL.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetByIdAsync(int id);
        Task<BookDto> AddAsync(BookCreateDto createDto);
        Task UpdateAsync(int id, BookUpdateDto updateDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(int authorId);
        Task<IEnumerable<BookDto>> SearchBooksAsync(string searchString);
        Task<IEnumerable<BookDto>> GetBooksByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }

}
