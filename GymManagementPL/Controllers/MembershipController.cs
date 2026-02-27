using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberShipsViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class MembershipController(IMemberShipService _memberShipService) : Controller
    {
        public ActionResult Index()
        {
            var memberShips = _memberShipService.GetAllMemberShips();
            return View(memberShips);
        }

        public ActionResult Create()
        {
            LoadDropDownLists();
            return View("CreateMembership");
        }

        [HttpPost]
        public ActionResult Create(CreateMemberShipVM model)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownLists();
                return View("CreateMembership", model);
            }

            var result = _memberShipService.CreateMemberShip(model);
            if (result)
            {
                TempData["SuccessMessage"] = "Membership created successfully.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Failed to create membership. The member may already have an active membership or the selected member/plan is invalid.";
            LoadDropDownLists();
            return View("CreateMembership", model);
        }

        public ActionResult Cancel(int id)
        {
            var result = _memberShipService.DeleteMemberShip(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Membership cancelled successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to cancel membership. The member may not have an active membership.";
            }
            return RedirectToAction("Index");
        }




        #region Helper Methods

        public void LoadDropDownLists()
        {
            var members = _memberShipService.GetAllMemberForSelectList();
            var plans = _memberShipService.GetAllPlanForSelectList();
            ViewBag.Members = new SelectList(members ,"Id","Name");
            ViewBag.Plans = new SelectList(plans ,"Id","Name");
        }

        #endregion
    }
}
