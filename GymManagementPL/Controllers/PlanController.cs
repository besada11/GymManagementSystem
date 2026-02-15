using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
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

        //Edtit plan    
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanToUpdate(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan cannot be updated.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdatePlanVM updatePlan)
        {
            if (!ModelState.IsValid)
            {
                TempData["WrongData"] = "Please correct the errors in the form.";
                return View(updatePlan);
            }
            var result = _planService.UpdatePlan(id, updatePlan);
            if (result)
            {
                TempData["SuccessMessage"] = "Plan updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update the plan. Please try again.";
                return View(updatePlan);
            }
        }

        //Activate or Deactivate plan
        [HttpPost]
        public ActionResult Activate(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.ToggleStatus(id);
            if (plan)
            {
                TempData["SuccessMessage"] = "Plan status updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update plan status.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
