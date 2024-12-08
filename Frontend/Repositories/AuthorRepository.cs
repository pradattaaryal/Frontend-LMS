using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Frontend.Models;

namespace Frontend.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly HttpClient _httpClient;

        public AuthorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Author>>("https://localhost:7192/api/Author");
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Author>($"https://localhost:7192/api/Author/{id}");
        }

        public async Task<bool> AddAuthorAsync(Author author)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7192/api/Author", author);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7192/api/Author/{author.AuthorID}", author);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7192/api/Author/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
