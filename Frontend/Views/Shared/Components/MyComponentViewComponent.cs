using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using Frontend.Repositories;
using System.Threading.Tasks;

public class MyComponentViewComponent : ViewComponent
{
    private readonly IDashboardRepository _dashboardRepository;

    public MyComponentViewComponent(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

     
    public async Task<IViewComponentResult> InvokeAsync()
    {

        var overdueBorrowers = await _dashboardRepository.GetOverdueBorrowersAsync();
    

        // Return the view with the model
        return View(overdueBorrowers);
    }
}
