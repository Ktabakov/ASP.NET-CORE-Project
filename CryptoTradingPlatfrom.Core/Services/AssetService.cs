using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatfrom.Core.Models.Assets;
using CryptoTradingPlatfrom.Core.Models.Api;
using CryptoTradingPlatform.Core.Contracts;

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
                return (success, "The Asset can't be empty");
            }
            if (data.Assets.Any(c => c.Ticker == model.Ticker))
            {
                return (success, "That Asset already exists on the plattform");
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
                error = ex.Message;
                throw;
            }
            return (success, error);
        }

        public async Task<decimal> CalculateTransaction(BuyAssetFormModel model)
        {
            string sellAssetTicker = data.Assets.FirstOrDefault(a => a.Id == model.SellAssetId).Ticker;
            string buyAssetTicker = data.Assets.FirstOrDefault(a => a.Id == model.BuyAssetId).Ticker;

            List<string> tickers = new List<string> { buyAssetTicker, sellAssetTicker };
            BuyAssetResponseModel responseModel = await apiService.GetPrices(tickers);

            decimal sellAssetPriceUSD = model.SellAssetQyantity * responseModel.SellAssetPrice;
            decimal buyAssetQuantity = sellAssetPriceUSD / responseModel.BuyAssetPrice;
            
            return buyAssetQuantity;
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

        public List<string> GetTickers()
        {
            return data.Assets.Select(x => x.Ticker).ToList();
        }

        public SwapAssetsListViewModel ListForSwap(string name)
        {
            SwapAssetsListViewModel modelList = new SwapAssetsListViewModel();
            modelList.Assets = data.Assets.Select(c => new SwapAssetViewModel{ AssetName = c.Name, AssetQuantity = c.CirculatingSupply, AssetId = c.Id}).ToList();
            modelList.UserMoney = 10000;
            //get assets from user. I need their quantity. For this method I wont need his usd money
            return modelList;
        }

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

        public bool SaveSwap(BuyAssetFormModel model)
        {
            return false;
        }
    }
}
