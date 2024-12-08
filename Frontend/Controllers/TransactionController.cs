using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Presentation.Repositories;
using Frontend.Models;

namespace Presentation.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // Get all transactions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
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

            return View(transaction);
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
