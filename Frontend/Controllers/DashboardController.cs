using Frontend.Models;
using Frontend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dashboardData = await _dashboardRepository.GetDashboardDataAsync();
            return View(dashboardData);
        }

    }
}
    