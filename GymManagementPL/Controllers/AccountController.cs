using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class AccountController(IAccountService _accountService, SignInManager<ApplicationUser> _signInManager) : Controller
    {
        // Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var User = _accountService.ValidateUser(loginVM);
            if (User == null)
            {
                ModelState.AddModelError("Invalid Login", "Invalid Email Or Password");
                return View(loginVM);
            }

            var Result =_signInManager.PasswordSignInAsync(User, loginVM.Password, loginVM.RememberMe, false).Result;
            if (Result.IsNotAllowed) 
                ModelState.AddModelError("Invalid Login", "Your Account Is Not Allowed");

            if(Result.IsNotAllowed)
                ModelState.AddModelError("Invalid Login", "Your Account Is Locked");
            if(Result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View(loginVM);
        }

        //Logout
        [HttpPost]
        public ActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }

        //Access Denied
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
