using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
