using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Classes
{
    public class AccountService (UserManager<ApplicationUser> _userManager) : IAccountService
    {
        public ApplicationUser ValidateUser(LoginVM loginVM)
        {
            var user = _userManager.FindByEmailAsync(loginVM.Email).Result;
            if(user is null) return null;

            var IsPasswordValid = _userManager.CheckPasswordAsync(user, loginVM.Password).Result;
            return IsPasswordValid? user : null;

        }
    }
}
