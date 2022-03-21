using CryptoTradingPlatform.Constants;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Areas.Admin.Controllers
{
    public class ControlController : BaseController
    {
        private readonly IUserService userService;
        private readonly IAdminService adminService;

        public ControlController(IUserService _userService, IAdminService _adminService)
        {
            userService = _userService;
            adminService = _adminService;
        }
        public async Task<IActionResult> AllApplicatnts()
        {
            List<ManagerApplicationViewModel> model = await adminService.GetAllApplications();
            return View(model);
        }
        public async Task<IActionResult> AllUsers()
        {
            List<string> roles = await adminService.GetAllRoles();
            ViewBag.Roles = roles;
            List<UserViewModel> users = await adminService.GetAllUsers();

            return View(users);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeRole(string role, string userId)
        {
            (bool result, string error) = await adminService.ChangeRole(role, userId);

            if (!result)
            {
                return Json(new { success = false, responseText = error});
            }

            return Json(new { success = true });
        }
        public async Task<IActionResult> DeleteApplication(string id)
        {
            if (id == null)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View();
            }
            bool success = await adminService.DeleteManagerApplication(id);
            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View();
            }

            TempData[MessageConstants.Success] = "Application Deleted";
            return Redirect("/admin");
        }

        public async Task<IActionResult> PromoteApplicant(string id)
        {
            if (id == null)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View();
            }
            bool success = await adminService.PromoteUserToManager(id);
            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View();
            }

            TempData[MessageConstants.Success] = "Application Approved";
            return Redirect("/admin");
        }

        public async Task<IActionResult> Statistics()
        {
            StatisticsViewModel model = await adminService.GetStatistics();
            return View(model);
        }
    }
}
