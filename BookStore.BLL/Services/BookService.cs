using AutoMapper;
using BookStore.BLL.DTOs;
using BookStore.DAL.Entities;
using BookStore.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BookDto> AddAsync(BookCreateDto createDto)
        {
            var book = _mapper.Map<Book>(createDto);
            await _unitOfWork.Books.AddAsync(book);
           await _unitOfWork.CompleteAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)           
                throw new Exception($"Book with ID {id} not found.");
            

            _unitOfWork.Books.Delete(book);
            await _unitOfWork.CompleteAsync();
        }

        

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllWithAuthors().ToListAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public IQueryable<Book> GetAllWithAuthors()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(int authorId)
        {
            var books = await _unitOfWork.Books.GetBooksByAutherAsync(authorId);
            return _mapper.Map<IEnumerable<BookDto>>(books);

        }

        public async Task<IEnumerable<BookDto>> GetBooksByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var books = await _unitOfWork.Books.GetBooksByPriceRangeAsync(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<BookDto>>(books);

        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            return _mapper.Map<BookDto>(book);

        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string searchString)
        {
            var books = await _unitOfWork.Books.SearchBooksAsync(searchString);
            return _mapper.Map<IEnumerable<BookDto>>(books);

        }

        public async Task UpdateAsync(int id, BookUpdateDto updateDto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
             _mapper.Map(updateDto, book);
             _unitOfWork.Books.Update(book);

            await _unitOfWork.CompleteAsync();
        }
    }
}
