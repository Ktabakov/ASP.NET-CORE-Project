using CryptoTradingPlatform.Core.Models.Admins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }

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
