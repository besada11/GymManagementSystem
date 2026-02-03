using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController(IMemberService _memberService) : Controller
    {
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();   
            return View(members);
        }
    }
}
