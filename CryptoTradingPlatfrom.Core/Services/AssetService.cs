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

        public AssetService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task<(bool, string)> AddAsset(CryptoResponseModel model)
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
                await data.Assets.AddAsync(asset);
                await data.SaveChangesAsync();
                success = true;
            }
            catch (Exception ex)
            {
                error = "Unexpected error";
                throw;
            }
            return (success, error);
        }

        public async Task<List<string>> GetAllAssetTickers()
        {
            return await data.Assets.Select(c => c.Ticker).ToListAsync();
        }

        public async Task<AssetDetailsViewModel> GetDetails(string assetName)
        {
            return await data.
                Assets
                .Where(c => c.Name == assetName)
                .Select(c => new AssetDetailsViewModel
                {
                    Description = c.Description,
                    Logo = c.ImageURL,
                    Name = c.Name,
                    Ticker = c.Ticker
                })
               .FirstAsync();
        }

        public async Task<List<string>> GetIds()
        {
            return await data.Assets.Select(x => x.Id).ToListAsync();
        }

        public async Task<List<string>> GetTickers()
        {
            return await data.Assets.Select(x => x.Ticker).ToListAsync();
        }

        public async Task<decimal> GetUserMoney(string? name)
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
            var user = await data.Users.Where(c => c.UserName == name).FirstOrDefaultAsync();
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
        public async Task<bool> RemoveAsset(string assetName)
        {
            var asset = await data.Assets.FirstOrDefaultAsync(a => a.Name == assetName);
            bool success = false;

            try
            {
                data.Remove(asset);
                await data.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {}
            return success;
            
        }

       /* public async Task<List<CryptoResponseModel>> CheckIfFavorites(List<CryptoResponseModel> cryptos, string userName)
        {
            var user = data.Users.FirstOrDefault(c => c.UserName == userName);

            for (int i = 0; i < cryptos.Count; i++)
            {
                var cr = cryptos[i];
                var assetId = data.Assets.FirstOrDefault(c => c.Ticker == cr.Ticker).Id;
                cr.IsInFavorites = data.UserFavorites.FirstOrDefault(c => c.ApplicationUserId == user.Id && c.AssetId == assetId) == null;
            }
            return cryptos;
        }*/

        public async Task<List<string>> GetAllFavoritesTickers(string? name)
        {
            return await data.UserFavorites
                .Where(c => c.User.UserName == name)
                .Include(c => c.Asset)
                .Select(c => c.Asset.Ticker)
                .ToListAsync();
        }

        public bool IsAssetFavorite(string userName, string assetTicker)
        {
             return data
                .UserFavorites
                .Where(u => u.User.UserName == userName)
                .Any(c => c.Asset.Ticker == assetTicker);
        }
    }
}
