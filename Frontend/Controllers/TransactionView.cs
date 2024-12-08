using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Presentation.Repositories;
using Frontend.Models;

namespace Presentation.Controllers
{
    public class TransactionViewController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionViewController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync();
            return View(transactions);
        }
          
    }
}
