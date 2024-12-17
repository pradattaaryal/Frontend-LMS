using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Presentation.Repositories;
using Frontend.Models;
using Frontend.Repositories;

namespace Presentation.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IBookRepository _BookRepository;
        public TransactionController(ITransactionRepository transactionRepository, IStudentRepository studentRepository, IBookRepository BookRepository)
        {
            _transactionRepository = transactionRepository;
            _studentRepository = studentRepository;
            _BookRepository = BookRepository;
        }

        // Get all transactions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            var students = await _studentRepository.GetAllStudentsAsync();
            var Book = await _BookRepository.GetAllBooksAsync();
            var bookData = Book.Select(b => new {b.BookId,b.Title}).ToList();
            HttpContext.Session.SetObjectAsJson("bookData", bookData);
            var studentData = students.Select(s => new { s.StudentId, s.Name }).ToList();
            HttpContext.Session.SetObjectAsJson("StudentData", studentData);
            return View(transactions);
        }




        // Add a transaction (submit form)
        [HttpPost]
        public async Task<IActionResult> AddTransaction(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var success = await _transactionRepository.AddTransactionAsync(transaction);
                if (success)
                {
                    TempData["message"] = "Transaction added successfully!";
                    TempData["isSuccess"] = true;
                    return RedirectToAction("Index", "TransactionView");
                }

                TempData["message"] = "Failed to add transaction.";
                TempData["isSuccess"] = false;
                return RedirectToAction("Index");
            }

            return View(transaction);
        }


        // Edit a transaction (renders edit view)
        [HttpGet]
        public async Task<IActionResult> EditTransaction(int id)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();

             return RedirectToAction("Index", "TransactionView"); ;
        }

        // Edit a transaction (submit changes)
        [HttpPost]
        public async Task<IActionResult> EditTransaction(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var success = await _transactionRepository.UpdateTransactionAsync(transaction);
                if (success)
                    return RedirectToAction("Index");
            }

            return View(transaction);
        }
        [HttpGet]
     
        // Delete a transaction
        [HttpPost]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var success = await _transactionRepository.DeleteTransactionAsync(id);
            return RedirectToAction("Index");
        }

         

    }
}
