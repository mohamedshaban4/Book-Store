using AutoMapper;
using BookStore.BLL.DTOs;
using BookStore.DAL.Entities;
using BookStore.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthorDto> AddAsync(AuthorCreateDto createDto)
        {
            var author = _mapper.Map<Author>(createDto);
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
                throw new Exception("Author is not found");

            _unitOfWork.Authors.Delete(author);
            await _unitOfWork.CompleteAsync();
        }


        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);

            // Populate the BooksCount property for each author
            foreach (var authorDto in authorDtos)
            {
                var booksCount = await _unitOfWork.Books.CountAsync(b => b.AuthorId == authorDto.Id);
                authorDto.BooksCount = booksCount;
            }

            return authorDtos;
        }


        public async Task<AuthorDto> GetAuthorByNameAsync(string name)
        {
            var author = await _unitOfWork.Authors.GetAuthorByName(name);
            if (author == null)
                throw new Exception("Author is not found");

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> GetByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
            {
                throw new Exception("Author is not found");
            }

            var booksCount = await _unitOfWork.Books.CountAsync(b => b.AuthorId == id);
            var authorDto = _mapper.Map<AuthorDto>(author);
            authorDto.BooksCount = booksCount;

            return authorDto;
        }


        public async Task UpdateAsync(int id, AuthorCreateDto updateDto)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null)
                throw new Exception("Author is not found");

            _mapper.Map(updateDto, author);
            _unitOfWork.Authors.Update(author);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<string> GetAuthorNameByBookIdAsync(int bookId)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookId);
            if (book == null)
            {
                // Handle the case where the book is not found
                return null;
            }

            var author = await _unitOfWork.Authors.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                // Handle the case where the author is not found
                return null;
            }

            return author.Name;
        }

        public async Task<AuthorDto> SearchAuthorsAsync(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
            {
                var author = await _unitOfWork.Authors.SearchAuthorsAsync(keyWord);
                return _mapper.Map<AuthorDto>(author);
            }
            else
            {
                // Handle the case when the keyword is empty or null
                return null;
            }
        }

    }
}
