using CryptoTradingPlatform.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CryptoTradingPlatform.Test.Controllers
{
    public class ManagerControllerTest
    {
        [Test]
        public async Task IndexShouldReturnView()
        {
            var managerController = new ManagerController();

            var result = managerController.Index();

            Assert.NotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }
    }
}
