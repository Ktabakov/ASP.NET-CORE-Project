using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatfrom.Core.Models.Assets;
using CryptoTradingPlatfrom.Core.Models.Api;
using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Trading;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class AssetService : IAssetService
    {
        private readonly ApplicationDbContext data;
        private readonly ICryptoApiService apiService;

        public AssetService(ApplicationDbContext _data, ICryptoApiService _apiService)
        {
            data = _data;
            apiService = _apiService;
        }

        public (bool, string) AddAsset(CryptoResponseModel model)
        {
            bool success = false;
            string error = string.Empty;
            if (model == null)
            {
                return (success, "The Asset can't be an empty field");
            }
            if (data.Assets.Any(c => c.Ticker == model.Ticker))
            {
                return (success, "This asset already exists on the plattform");
            }
            Asset asset = new Asset()
            {
                CirculatingSupply = model.CirculatingSupply,
                Description = model.Description,
                ImageURL = model.Logo,
                Name = model.Name,
                Ticker = model.Ticker,
            };
            try
            {
                data.Assets.Add(asset);
                data.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                error = "Unexpected error";
                throw;
            }
            return (success, error);
        }

        public async Task<decimal> CalculateTransaction(BuyAssetFormModel model)
        {
            string sellAssetTicker = data
                .Assets
                .FirstOrDefault(a => a.Id == model.SellAssetId)
                .Ticker;

            string buyAssetTicker = data
                .Assets
                .FirstOrDefault(a => a.Id == model.BuyAssetId)
                .Ticker;

            List<string> tickers = new List<string> { buyAssetTicker, sellAssetTicker };
            BuyAssetResponseModel responseModel = await apiService.GetPrices(tickers);

            decimal sellAssetPriceUSD = model.SellAssetQyantity * responseModel.SellAssetPrice;
            decimal buyAssetQuantity = sellAssetPriceUSD / responseModel.BuyAssetPrice;
            
            return buyAssetQuantity;
        }

        public async Task<List<string>> GetAllAssetTickers()
        {
            return await data.Assets.Select(c => c.Ticker).ToListAsync();
        }

        public AssetDetailsViewModel GetDetails(string assetName)
        {
            return data.
                Assets
                .Where(c => c.Name == assetName)
                .Select(c => new AssetDetailsViewModel
                {
                    Description = c.Description,
                    Logo = c.ImageURL,
                    Name = c.Name,
                    Ticker = c.Ticker
                })
               .First();
        }

        public List<string> GetIds()
        {
            return data.Assets.Select(x => x.Id).ToList();
        }

        public async Task<List<string>> GetTickers()
        {
            return await data.Assets.Select(x => x.Ticker).ToListAsync();
        }

        public decimal GetUserMoney(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0M;
            }
            return Math.Round(
                 data
                .Users
                .Where(c => c.UserName == name)
                .FirstOrDefault()
                .Money, 2);
        }

        public async Task<SwapAssetsListViewModel> GetUserAssets(string name)
        {
            SwapAssetsListViewModel modelList = new SwapAssetsListViewModel();
            var user = data.Users.Where(c => c.UserName == name).FirstOrDefault();
            modelList.Assets = data
                .UserAssets
                .Where(c => c.ApplicationUserId == user.Id)        
                .Where(c => c.Quantity > 0)
                .Select(c => new SwapAssetViewModel
                {
                    AssetName = c.Asset.Name,
                    AssetQuantity = Convert.ToDecimal(c.Quantity),
                    AssetId = c.AssetId,
                    ImageUrl = c.Asset.ImageURL
                })
               .ToList();

            modelList.UserMoney = data
                .Users
                .FirstOrDefault(c => c.UserName == name)
                .Money;

            return modelList;
        }

        //maybe to cascade delete - remove from userfavorites also
        public bool RemoveAsset(string assetName)
        {
            var asset = data.Assets.FirstOrDefault(a => a.Name == assetName);
            bool success = false;

            try
            {
                data.Remove(asset);
                data.SaveChanges();
                success = true;
            }
            catch (Exception)
            {}
            return success;
            
        }

        public async Task<List<CryptoResponseModel>> CheckIfFavorites(List<CryptoResponseModel> cryptos, string userName)
        {
            var user = data.Users.FirstOrDefault(c => c.UserName == userName);

            for (int i = 0; i < cryptos.Count; i++)
            {
                var cr = cryptos[i];
                var assetId = data.Assets.FirstOrDefault(c => c.Ticker == cr.Ticker).Id;
                cr.IsInFavorites = data.UserFavorites.FirstOrDefault(c => c.ApplicationUserId == user.Id && c.AssetId == assetId) == null;
            }
            return cryptos;
        }

        public async Task<List<string>> GetAllFavoritesTickers(string? name)
        {
            return await data.UserFavorites
                .Where(c => c.User.UserName == name)
                .Include(c => c.Asset)
                .Select(c => c.Asset.Ticker)
                .ToListAsync();
        }
    }
}
