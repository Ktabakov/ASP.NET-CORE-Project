using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    public class BaseController : Controller
    {
       
    }
}
