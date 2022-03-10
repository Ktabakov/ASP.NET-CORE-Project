using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Data.Models;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class AssetService : IAssetService
    {
        private readonly ApplicationDbContext data;

        public AssetService(ApplicationDbContext _data)
        {
            data = _data;
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

        public List<string> GetIds()
        {
            return data.Assets.Select(x => x.Id).ToList();
        }

        public List<string> GetTickers()
        {
            return data.Assets.Select(x => x.Ticker).ToList();
        }
    }
}
