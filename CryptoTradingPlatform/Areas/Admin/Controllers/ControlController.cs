using CryptoTradingPlatform.Constants;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Areas.Admin.Controllers
{
    public class ControlController : BaseController
    {
        private readonly IUserService userService;

        public ControlController(IUserService _userService)
        {
            userService = _userService;
        }
        public async Task<IActionResult> AllApplicatnts()
        {
            List<ManagerApplicationViewModel> model = await userService.GetAllApplications();
            return View(model);
        }
        public async Task<IActionResult> AllUsers()
        {
            List<string> roles = await userService.GetAllRoles();
            ViewBag.Roles = roles;
            List<UserViewModel> users = await userService.GetAllUsers();

            return View(users);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeRole(string role, string userId)
        {
            (bool result, string error) = await userService.ChangeRole(role, userId);

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
            bool success = await userService.DeleteManagerApplication(id);
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
            bool success = await userService.PromoteUserToManager(id);
            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View();
            }

            TempData[MessageConstants.Success] = "Application Approved";
            return Redirect("/admin");
        }
    }
}
