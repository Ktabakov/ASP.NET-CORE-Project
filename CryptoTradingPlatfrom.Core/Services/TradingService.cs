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
            double quantityToDouble = Convert.ToDouble(model.Quantity); 

            if (asset == null)
            {
                return false;
            }

            decimal sum = model.Price * model.Quantity;

            if (user.Money < sum && model.Type == "Buy")
            {
                return false;
            }

            try
            {
                bool assetExists = data.UserAssets.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.AssetId == asset.Id) != null;

                if (model.Type == "Buy")
                {
                    if (!assetExists)
                    {
                        data.UserAssets.Add(new UserAsset() { ApplicationUserId = user.Id, AssetId = asset.Id, Quantity = quantityToDouble });
                    }
                    else
                    {
                        data.UserAssets.Where(c => c.ApplicationUserId == user.Id).First(c => c.AssetId == asset.Id).Quantity += quantityToDouble;
                    }
                    user.Money -= sum;
                }
                else if (model.Type == "Sell")
                {
                    if (!assetExists)
                    {
                        return false;
                    }
                    else
                    {
                        if (data.UserAssets.Where(c => c.ApplicationUserId == user.Id).First(c => c.AssetId == asset.Id).Quantity - quantityToDouble < 0)
                        {
                            return false;
                        }
                        data.UserAssets.Where(c => c.ApplicationUserId == user.Id).First(c => c.AssetId == asset.Id).Quantity -= quantityToDouble;
                    }
                    user.Money += sum;
                }
            }
            catch (Exception)
            {
                return false;
            }

            Transaction transaction = new Transaction()
            {
                ApplicationUser = user,
                Asset = asset,
                Date = DateTime.Now,
                Price = model.Price,
                Quantity = quantityToDouble,
                Type = model.Type,
            };

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

            return success;
        }
    }
}
