using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Frontend.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Frontend.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly HttpClient _httpClient;

        public StudentRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var Student = await _httpClient.GetFromJsonAsync<List<Student>>("https://localhost:7192/api/Student");
            return Student?.OrderByDescending(s=> s.StudentId).ToList();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Student>($"https://localhost:7192/api/Student/{id}");
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7192/api/Student", student);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7192/api/Student/{student.StudentId}", student);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7192/api/Student/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
