using Frontend.Models;
using Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthService _authService;
        private readonly HttpClient _httpClient;
        public AuthenticationController(AuthService authService)
        {
            _authService = authService;
        }

        // Default login page (GET: /Authentication/Index)
        [HttpGet]
        public IActionResult Index()
        {
            // Check if the user is already logged in (token exists in cookies)
            var token = HttpContext.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Books"); // Redirect to the books page if already logged in
            }

            return View("Login"); // Show the login view
        }

        // Handles login requests (POST: /Authentication/Login)
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            HttpContext.Session.SetString("User", model.Username);
            // Check if the user is already logged in
            var token = HttpContext.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Books");
            }

            // Validate login credentials
            if (ModelState.IsValid)
            {
                var generatedToken = await _authService.LoginAsync(model.Username, model.Password);
                if (!string.IsNullOrEmpty(generatedToken))
                {
                    // Store token in cookies
                    HttpContext.Response.Cookies.Append("token", generatedToken);
                    return RedirectToAction("Index", "Dashboard");  
                }

                // If credentials are invalid, display an error message
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View("Login", model); // Reload the login page with the model and errors
        }

        // Handles logout requests (GET: /Authentication/Logout)
        public IActionResult Logout()
        {
            // Remove the token from cookies
            HttpContext.Response.Cookies.Delete("token");

            return RedirectToAction("Index"); // Redirect to the login page
        }
    }
}
