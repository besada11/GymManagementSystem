using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController(IPlanService _planService) : Controller
    {
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlan();
            return View(plans);
        }
    }
}
