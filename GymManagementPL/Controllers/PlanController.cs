using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController(IPlanService _planService) : Controller
    {
        //Get all plans
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlan();
            return View(plans);
        }

        //Plan details
        public IActionResult Details(int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanById(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
    }
}
