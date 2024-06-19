using AutoMapper;
using BookStore.BLL.DTOs;
using BookStore.BLL.Services;
using BookStore.DAL.Entities;
using BookStore.PL.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.PL.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, ILogger<BookController> logger, IAuthorService authorService, IMapper mapper)
        {
            _bookService = bookService;
            _logger = logger;
            _authorService = authorService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
              return  RedirectToAction(nameof(Index));

            var books = await _bookService.SearchBooksAsync(searchString);
            return View("Index", books); // Assuming you want to render the same view for search results
        }



        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound("Book is not found");

            // Fetch the author's name by book ID
            var authorName = await _authorService.GetAuthorNameByBookIdAsync(book.Id);

            // Set the author's name in the book DTO
            book.AuthorName = authorName;

            return View(book);
        }



        public async Task<IActionResult> Create()
        {
            var authors = await _authorService.GetAllAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateDto bookCreateDto)
        {
            if (ModelState.IsValid)
            {

                if(bookCreateDto.Image is not null)
                {
                    var fileName = DocumentSetting.UploadFile(bookCreateDto.Image, "Images");
                    bookCreateDto.ImageUrl = $"/Files/BookImages/{fileName}";
                }
                await _bookService.AddAsync(bookCreateDto);
                return RedirectToAction(nameof(Index));
            }

            // Re-fetch authors if model state is invalid to repopulate the dropdown
            var authors = await _authorService.GetAllAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "Name");

            return View(bookCreateDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var editDto = new BookUpdateDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Price = book.Price,
                AuthorId = book.AuthorId,
                ImageUrl = book.ImageUrl
            };

            return View(editDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookUpdateDto editDto, IFormFile imageFile)
        {
            if (id != editDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(editDto);
            }

            if (imageFile != null)
            {
                var imageUrl = DocumentSetting.UploadFile(imageFile, "BookImages");
                editDto.ImageUrl = imageUrl;
            }

            await _bookService.UpdateAsync(id, editDto);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound("Book is not found");

            // Fetch the author's name and set it in the DTO
            book.AuthorName = await _authorService.GetAuthorNameByBookIdAsync(id);

            return View(book);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }

}

