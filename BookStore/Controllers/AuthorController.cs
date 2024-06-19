using BookStore.BLL.DTOs;
using BookStore.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.PL.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;

        //private readonly IBookService _bookService;

        public AuthorController(IAuthorService authorService, IBookService bookService, ILogger<AuthorController> logger)
        {
            _authorService = authorService;
            _logger = logger;
            //_bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAsync();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorCreateDto authorCreateDto)
        {
            if (ModelState.IsValid)
            {
                await _authorService.AddAsync(authorCreateDto);
                return RedirectToAction(nameof(Index));
            }

            return View(authorCreateDto);
        }
        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            var authorEditDto = new AuthorCreateDto
            {
                Id = author.Id,
                Name = author.Name,
                BooksCount = author.BooksCount // Populate the number of books
            };

            return View(authorEditDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorCreateDto authorCreateDto)
        {
            if (id != authorCreateDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var author = await _authorService.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                await _authorService.UpdateAsync(id, authorCreateDto);
                return RedirectToAction(nameof(Index));
            }

            return View(authorCreateDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
