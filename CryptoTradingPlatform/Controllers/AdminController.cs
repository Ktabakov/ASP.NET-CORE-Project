using CryptoTradingPlatform.Models.Admins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(AddAdminFormModel model)
        {
            if (ModelState.IsValid)
            {
                //Save the user as an Admin
                return Redirect("/");
            }
            else
            {
                return View();
            }
        }
    }
}
