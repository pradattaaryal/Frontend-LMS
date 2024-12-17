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

            var Transaction = await _httpClient.GetFromJsonAsync<List<Transaction>>("https://localhost:7192/api/Transaction");
            return Transaction?.OrderByDescending(T=>T.TransactionId).ToList();
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
        public async Task<List<Transaction>> GetTransactionsByBookNameAsync(string bookName)
        {
            try
            {
                // Call the API and fetch the data
                var response = await _httpClient.GetAsync($"https://localhost:7192/api/Transaction/book?bookName={bookName}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API returned an error: {response.StatusCode}");
                }

                var transactions = await response.Content.ReadFromJsonAsync<List<Transaction>>();

                return transactions?.OrderByDescending(t => t.TransactionId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching transactions: {ex.Message}");
            }
        }


    }
}
