using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Set the base address for the HttpClient
            _httpClient.BaseAddress = new Uri("https://localhost:7192/api/");
        }

        // Method to handle login
        public async Task<string> LoginAsync(string username, string password)
        {
            // Prepare the payload
            var payload = new { Username = username, Password = password };

            // Send POST request to login endpoint
            var response = await SendPostRequest("Auth/login", payload);

            // Extract and return the token from the response
            var token = await ExtractTokenFromResponse(response);
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token not found in the response.");
            }

            return token;
        }

        // Helper Method: Send POST request
        private async Task<HttpResponseMessage> SendPostRequest(string endpoint, object data)
        {
            // Serialize the data to JSON
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Send POST request
            var response = await _httpClient.PostAsync(endpoint, content);

            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Request failed. Status Code: {response.StatusCode}, Error: {errorContent}");
            }

            return response;
        }

        // Helper Method: Extract token from the response
        private async Task<string> ExtractTokenFromResponse(HttpResponseMessage response)
        {
            // Read response content as string
            var result = await response.Content.ReadAsStringAsync();

            // Deserialize the response content to extract the token
            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(result);
            return tokenResponse?.token;
        }
    }
}
