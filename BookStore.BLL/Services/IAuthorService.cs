using BookStore.BLL.DTOs;

namespace BookStore.BLL.Services
{
    public interface IAuthorService
    {
        Task<AuthorDto> GetAuthorByNameAsync(string name);
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto> SearchAuthorsAsync(string keyWord);
        Task<string> GetAuthorNameByBookIdAsync(int bookId);
        Task<AuthorDto> GetByIdAsync(int id);
        Task<AuthorDto> AddAsync(AuthorCreateDto createDto);
        Task UpdateAsync(int id, AuthorCreateDto updateDto);
        Task DeleteAsync(int id);


    }
}
