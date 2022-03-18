using CryptoTradingPlatform.Core.Models.Admins;
using CryptoTradingPlatform.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddAdminFormModel model)
        {
            var roleName = "Admin";
            var roleExists = await roleManager.RoleExistsAsync(roleName);

            if (roleExists)
            {
                var user = userManager.GetUserAsync(User);
                //var result = await userManager.AddToRoleAsync(user, roleName);
            }

            
                return View();
            
        }     
       
    }
}
