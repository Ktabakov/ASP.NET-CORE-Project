using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager, Administrator")]
    [Area("Manager")]
    public class BaseController : Controller
    {
       
    }
}
