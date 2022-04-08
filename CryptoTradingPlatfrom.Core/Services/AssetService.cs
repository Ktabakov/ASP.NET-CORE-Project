using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatfrom.Core.Models.Assets;
using CryptoTradingPlatfrom.Core.Models.Api;
using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Trading;
using Microsoft.EntityFrameworkCore;
using CryptoTradingPlatform.Infrastructure.Data.Repositories;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class AssetService : IAssetService
    {
        private readonly IApplicatioDbRepository repo;

        public AssetService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<(bool, string)> AddAsset(CryptoResponseModel model)
        {
            bool success = false;
            string error = string.Empty;
           
            if (repo.All<Asset>().Any(c => c.Ticker == model.Ticker))
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
                await repo.AddAsync<Asset>(asset);
                await repo.SaveChangesAsync();
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
            return await repo.All<Asset>().Select(c => c.Ticker).ToListAsync();
        }

        public async Task<AssetDetailsViewModel> GetDetails(string assetName)
        {
            return await repo.All<Asset>()
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
            return await repo.All<Asset>().Select(x => x.Id).ToListAsync();
        }

        public async Task<decimal> GetUserMoney(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0M;
            }
            return Math.Round(
                 repo
                .All<ApplicationUser>()
                .Where(c => c.UserName == name)
                .FirstOrDefault()
                .Money, 2);
        }

        public async Task<SwapAssetsListViewModel> GetUserAssets(string name)
        {
            SwapAssetsListViewModel modelList = new SwapAssetsListViewModel();
            var user = await repo.All<ApplicationUser>().Where(c => c.UserName == name).FirstOrDefaultAsync();
            modelList.Assets = await repo
                .All<UserAsset>()
                .Where(c => c.ApplicationUserId == user.Id)
                .Where(c => c.Quantity > 0)
                .Select(c => new SwapAssetViewModel
                {
                    AssetName = c.Asset.Name,
                    AssetQuantity = c.Quantity,
                    AssetId = c.AssetId,
                    ImageUrl = c.Asset.ImageURL
                })
               .ToListAsync();

            modelList.UserMoney = repo
                .All<ApplicationUser>()
                .FirstOrDefault(c => c.UserName == name)
                .Money;

            return modelList;
        }

        public async Task<bool> RemoveAsset(string assetName)
        {
            var asset = await repo.All<Asset>().FirstOrDefaultAsync(a => a.Name == assetName);
            bool success = false;

            try
            {
                repo.Delete<Asset>(asset);
                await repo.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {
                return success;
            }
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
            return await repo
                .All<UserFovorites>()
                .Where(c => c.User.UserName == name)
                .Include(c => c.Asset)
                .Select(c => c.Asset.Ticker)
                .ToListAsync();
        }

        public bool IsAssetFavorite(string userName, string assetTicker)
        {
            return repo.All<UserFovorites>()
               .Where(u => u.User.UserName == userName)
               .Any(c => c.Asset.Ticker == assetTicker);
        }

        public async Task<bool> IsAssetOwned(string assetName)
        {
            var asset = await repo.All<Asset>().FirstOrDefaultAsync(a => a.Name == assetName);

            return repo
                .All<UserAsset>()
                 .Any(c => c.AssetId == asset.Id);
        }

    }
}
