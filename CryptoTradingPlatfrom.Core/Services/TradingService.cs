using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Models.Trading;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Assets;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class TradingService : ITradingService
    {
        private readonly ApplicationDbContext data;
        private readonly ICryptoApiService cryptoService;
        public TradingService(ApplicationDbContext _data, ICryptoApiService _cryptoService)
        {
            data = _data;
            cryptoService = _cryptoService;
        }

        public async Task<bool> SaveSwap (BuyAssetFormModel model, string userName)
        {
            var user = data.Users.FirstOrDefault(c => c.UserName == userName);
            var sellAsset = data.Assets.FirstOrDefault(c => c.Id == model.SellAssetId);
            var buyAsset = data.Assets.FirstOrDefault(c => c.Id == model.BuyAssetId);
            var sellQuantity = Convert.ToDouble(model.SellAssetQyantity);
            var buyQuantity = Convert.ToDouble(model.BuyAssetQuantity);
            bool success = false;

            var buyCryptoResponseModel = await cryptoService.GetCryptos(new List<string> { buyAsset.Ticker });
            var buyAssetPrice = buyCryptoResponseModel.FirstOrDefault().Price;

            if (buyAsset == null || sellAsset == null)
            {
                return false;
            }

            bool sellAssetExists = data.UserAssets.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.AssetId == model.SellAssetId) != null;
            bool buyssetExists = data.UserAssets.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.AssetId == model.BuyAssetId) != null;

            if (!sellAssetExists || !buyssetExists)
            {
                return false;
            }

            var sellAssetUserQuantity = data
                .UserAssets
                .Where(c => c.ApplicationUserId == user.Id)
                .FirstOrDefault(c => c.AssetId == model.SellAssetId)
                .Quantity;

            if (sellAssetUserQuantity - sellQuantity < 0)
            {
                return false;
            }
            
            
            Transaction transaction = new Transaction()
            {
                ApplicationUserId = user.Id,
                AssetId = buyAsset.Id,
                Date = DateTime.Now,
                Price = buyAssetPrice,
                Quantity = buyQuantity,
                Type = "Buy"
            };

            try
            {
                data.UserAssets.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.AssetId == sellAsset.Id).Quantity -= sellQuantity;
                data.UserAssets.Where(c => c.ApplicationUserId == user.Id).FirstOrDefault(c => c.AssetId == buyAsset.Id).Quantity += buyQuantity;
                data.Transactions.AddAsync(transaction);
                data.SaveChangesAsync();
                success = true;
                
            }
            catch (Exception)
            {
                return false;
            }
            return success;
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
                data.Transactions.AddAsync(transaction);
                data.SaveChangesAsync();
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
