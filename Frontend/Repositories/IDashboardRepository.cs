using Frontend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashBoard> GetDashboardDataAsync();
        Task<List<OverdueBorrower>> GetOverdueBorrowersAsync();
    }
}
