using Frontend.Models;
using Frontend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public BooksController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
       
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            var Author=await _authorRepository.GetAllAuthorsAsync();
            var AuthorData = Author.Select(A => new { A.AuthorID,A.Name }).ToList();
            HttpContext.Session.SetObjectAsJson("AuthorData", AuthorData);
            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> Indexx(int BookId, string Title, int AuthorId, string Genre, string ISBN, int Quantity)
        {
            var book = new Book { BookId = BookId, Title = Title, AuthorId = AuthorId, Genre = Genre, ISBN = ISBN, Quantity = Quantity };
            ViewBag.IsEdit = true;
            ViewBag.Book = book;

            var books = await _bookRepository.GetAllBooksAsync();
            return View("Index", books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                var success = await _bookRepository.AddBookAsync(book);
                if (success)
                    return RedirectToAction("Index");
            }

            var books = await _bookRepository.GetAllBooksAsync();
            return View("Index", books);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            if (ModelState.IsValid)
            {
                var success = await _bookRepository.UpdateBookAsync(book);
                if (success)
                    return RedirectToAction("Index");
            }

            var books = await _bookRepository.GetAllBooksAsync();
            return View("Index", books);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var success = await _bookRepository.DeleteBookAsync(id);
            return RedirectToAction("Index");
        }
    }
}
