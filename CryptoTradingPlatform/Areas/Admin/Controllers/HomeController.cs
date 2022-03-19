using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
