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
                return RedirectToAction(nameof(Index));

            var member = _memberService.GetMemberDetails(id);
            if(member == null)
                return RedirectToAction(nameof(Index));

            return View(member);
        }

        //Health Record
        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));
            var healthRecord = _memberService.GetMemberHealthRecord(id);
            if (healthRecord == null)
                return RedirectToAction(nameof(Index));
            return View(healthRecord);
        }
    }
}
 