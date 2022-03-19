using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Areas.Manager.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
