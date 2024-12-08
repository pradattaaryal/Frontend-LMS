using Frontend.Models;
using Frontend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                var success = await _studentRepository.AddStudentAsync(student);
                if (success)
                    return RedirectToAction("Index");
            }

            var students = await _studentRepository.GetAllStudentsAsync(); // Reload students for the view
            return View("Index", students);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                var success = await _studentRepository.UpdateStudentAsync(student);
                if (success)
                    return RedirectToAction("Index");
            }

            var students = await _studentRepository.GetAllStudentsAsync(); // Reload students for the view
            return View("Index", students);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var success = await _studentRepository.DeleteStudentAsync(id);
            return RedirectToAction("Index");
        }
    }
}
