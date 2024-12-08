using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Frontend.Models;

namespace Frontend.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly HttpClient _httpClient;

        public BookRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Book>>("https://localhost:7192/api/Books");
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"https://localhost:7192/api/Books/{id}");
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7192/api/Books", book);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7192/api/Books/{book.BookId}", book);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7192/api/Books/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
