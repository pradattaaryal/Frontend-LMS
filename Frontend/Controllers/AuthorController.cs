using Frontend.Models;
using Frontend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return View(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Indexx(int AuthorID, string Name, string Bio)
        {
            var author = new Author { AuthorID = AuthorID, Name = Name, Bio = Bio };
            ViewBag.IsEdit = true;
            ViewBag.Author = author;

            // Fetch authors list and pass it to the view
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return View("Index", authors);
        }



        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                var success = await _authorRepository.AddAuthorAsync(author);
                if (success)
                    return RedirectToAction("Index");
            }

            var authors = await _authorRepository.GetAllAuthorsAsync(); // Reload authors for the view
            return View("Index", authors);
        }

        [HttpPost]
        public async Task<IActionResult> EditAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                var success = await _authorRepository.UpdateAuthorAsync(author);
                if (success)
                    return RedirectToAction("Index");
            }

            var authors = await _authorRepository.GetAllAuthorsAsync(); // Reload authors for the view
            return View("Index", authors);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var success = await _authorRepository.DeleteAuthorAsync(id);
            return RedirectToAction("Index");
        }
    }
}
