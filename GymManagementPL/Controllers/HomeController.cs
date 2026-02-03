using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController(IAnalyticsService _analyticsService) : Controller
    {
        public IActionResult Index()
        {
            var data = _analyticsService.GetAnalyticsData();
            return View(data);
        }
       
    }
}
