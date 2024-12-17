using Frontend.Models;
using Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthService _authService;
        private readonly HttpClient _httpClient;
        public AuthenticationController(AuthService authService, IHttpClientFactory httpClientFactory)
        {
            _authService = authService;
            _httpClient = httpClientFactory.CreateClient();
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
        [HttpGet]
        public IActionResult _SignUpModal()
        {
            return View(); // Returns the signup view
        }

        // POST: /Authentication/SignUp
        public async Task<IActionResult> _SignUpModal(SignUp model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync("https://localhost:7192/api/Auth/signup", model);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Authentication"); 
                    }

                    ModelState.AddModelError("", "Failed to sign up. Please try again.");
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                }
            }

            return View(model); // Reload the signup page with errors
        }
        // Handles login requests (POST: /Authentication/Login)
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            HttpContext.Session.SetString("User", model.Username);
           
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
