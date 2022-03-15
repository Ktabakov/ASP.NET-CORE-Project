using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class FavoritesController : Controller
    {

        [Authorize]
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
    }
}
