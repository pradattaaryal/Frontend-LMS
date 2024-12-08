using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Frontend.Models;

namespace Presentation.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly HttpClient _httpClient;

        public TransactionRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch all transactions
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Transaction>>("https://localhost:7192/api/Transaction");
        }

        // Fetch a transaction by ID
        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Transaction>($"https://localhost:7192/api/Transaction/{id}");
        }

        // Add a new transaction
        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7192/api/Transaction", transaction);
            return response.IsSuccessStatusCode;
        }

        // Update an existing transaction
        public async Task<bool> UpdateTransactionAsync(Transaction transaction)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7192/api/Transaction/{transaction.TransactionId}", transaction);
            return response.IsSuccessStatusCode;
        }

        // Delete a transaction
        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7192/api/Transaction/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
