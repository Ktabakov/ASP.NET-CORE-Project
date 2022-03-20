using CryptoTradingPlatform.Constants;
using CryptoTradingPlatform.Core.Models.Users;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService _userService)
        {
            userService = _userService;
        }

        public async Task<IActionResult> Create()
        {
            bool applicationSent = await userService.IsApplicationSent(User.Identity.Name);
            ViewBag.ApplicationSent = applicationSent;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddManagerFormModel model)
        {

            if (!ModelState.IsValid)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View(model);
            }

            (bool success, string error) = await userService.SendManagerApplication(User.Identity.Name, model);

            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = error;
                return View(model);
            }

            TempData[MessageConstants.Success] = "Application Sent Successfully!";
            return Redirect("/");
        }
    }
}
