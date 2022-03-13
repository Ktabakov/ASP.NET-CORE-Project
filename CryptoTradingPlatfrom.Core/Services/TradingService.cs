using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Models.Trading;
using CryptoTradingPlatfrom.Core.Contracts;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class TradingService : ITradingService
    {
        private readonly ApplicationDbContext data;
        public TradingService(ApplicationDbContext _data)
        {
            data = _data;
        }
        public bool SaveTransaction(TradingFormModel model, string userName)
        {
            var user = data.Users.FirstOrDefault(c => c.UserName == userName);
            var asset = data.Assets.FirstOrDefault(c => c.Name == model.Name);
            bool success = false;

            if (asset == null)
            {
                return false;
            }

            decimal sum = model.Price * model.Quantity;
            Transaction transaction = new Transaction()
            {
                ApplicationUser = user,
                Asset = asset,
                Date = DateTime.Now,
                Price = model.Price,
                Quantity = Convert.ToDouble(model.Quantity),
                Type = model.Type,
            };

            if (model.Type == "Buy")
            {
                
            }
            else if (model.Type == "Sell")
            {

            }

            try
            {
                data.Transactions.Add(transaction);
                data.SaveChanges();
                success = true;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
