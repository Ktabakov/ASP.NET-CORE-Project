using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    [Authorize(Roles = "Manager, Administrator")]
    public class ManagerController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
