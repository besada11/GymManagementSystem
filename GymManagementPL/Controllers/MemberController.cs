using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

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
            if (id <= 0)
            {
                TempData["ErrorMessage"] = " Id of Member Can Not Be 0 Or Negative ";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberDetails(id);
            if (member == null)
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


        //Create Member
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //Create Member
        [HttpPost]
        public ActionResult CreateMember(CreateMemberVM member)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Field");
                return View(nameof(Create), member);
            }
            bool Result = _memberService.CreateMember(member);
            if (Result)
            {
                TempData["SuccessMessage"] = " Member Created Successfully ";
            }
            else
            {
                TempData["ErrorMessage"] = "Email or Phone number already exists";
                return View(nameof(Create), member);
            }
            return RedirectToAction(nameof(Index));
        }

        //Edit Member
        [HttpGet]
        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = " Id of Member Can Not Be 0 Or Negative ";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberToUpdate(id);
            if (member == null) 
            {
                TempData["ErrorMessage"] = " Member Not Found ";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute] int id, MemberToUpdateVM member)
        {
            if (!ModelState.IsValid)
                return View(member);

            var result = _memberService.UpdateMemberDetails(id, member);
            if (result == true)
            {
                TempData["SuccessMessage"] = " Member Updated Successfully ";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Update Failed";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
 