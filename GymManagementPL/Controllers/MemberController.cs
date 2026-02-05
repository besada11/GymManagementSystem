using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController(IMemberService _memberService) : Controller
    {
        //Get All Members   
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();   
            return View(members);
        }

        //Get Member Details by Id
        public ActionResult MemberDetails(int id) 
        {
            if(id<= 0)
            {
                TempData["ErrorMessage"] = " Id of Member Can Not Be 0 Or Negative ";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberDetails(id);
            if(member == null)
            {
                TempData["ErrorMessage"] = " Member Not Found ";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        //Health Record
        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = " Id of Member Can Not Be 0 Or Negative ";
                return RedirectToAction(nameof(Index));
            }
            var healthRecord = _memberService.GetMemberHealthRecord(id);
            if (healthRecord == null)
            {
                TempData["ErrorMessage"] = " Member Not Found ";
                return RedirectToAction(nameof(Index));
            }
            return View(healthRecord);
        }


    }
}
 