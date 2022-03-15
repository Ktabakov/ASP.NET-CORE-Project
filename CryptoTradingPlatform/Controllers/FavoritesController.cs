using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Favorites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ITradingService tradingService;
        public FavoritesController(ITradingService _tradingService)
        {
            tradingService = _tradingService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Add(string ticker)
        {
            bool result = await tradingService.SaveToFavorites(ticker, User.Identity.Name);

            if (!ModelState.IsValid || result == false)
            {
                return Json(new { success = false });
            }
            
            return Json(new { success = true });

        }
    }
}
