using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Data.DataSeed
{
    public static class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
			try
			{
                var HasUsers= userManager.Users.Any();
                var HasRoles= roleManager.Roles.Any();

                if (HasUsers && HasRoles)
                    return false;

                if(!HasRoles)
                {
                    var rolses = new List<IdentityRole>()
                    {
                        new () { Name = "SuperAdmin" },
                        new () { Name = "Admin" }
                    };

                    foreach (var role in rolses)
                    {
                        if(!roleManager.RoleExistsAsync(role.Name!).Result)
                        {
                            roleManager.CreateAsync(role).Wait();
                        }
                    }
                }

                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Besada",
                        LastName = "Nabil",
                        UserName = "BesadaNabil",
                        Email = "BesadaNabil@gmail.com",
                        PhoneNumber = "01000000000",
                    };
                    userManager.CreateAsync(MainAdmin , "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Youssef",
                        LastName = "Mohamed",
                        UserName = "YoussefMohamed",
                        Email = "YoussefMohamed@gmail.com",
                        PhoneNumber = "01100000000",
                    };
                    userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();
                }

                return true;
            }
            catch (Exception ex)
			{
                Console.WriteLine($"Seed Failed: {ex.Message}");
                return false;
            }
        }
    }
}
