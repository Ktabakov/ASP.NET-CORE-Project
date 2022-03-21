using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    [Route("ErrorPage/{statuscode}")]
    public class ErrorPageController : BaseController
    {
        public IActionResult Index(int statuscode)
        {
            switch (statuscode)
            {
                case 404:
                    ViewData["Error"] = "Page Not Found";
                    break;
                default:
                    break;
            }
            return View("ErrorPage");
        }
    }
}
