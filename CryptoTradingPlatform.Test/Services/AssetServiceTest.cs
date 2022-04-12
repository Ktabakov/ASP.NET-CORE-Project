using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Infrastructure.Data.Repositories;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatform.Test.Services
{
    public class AssetServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
                .AddSingleton<IAssetService, AssetService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task AddAssetShouldReturnFlaseWhenAssetAlreadyExists()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var model = new CryptoResponseModel()
            {
                CirculatingSupply = 123,
                Description = "123",
                Logo = "123",
                MarketCap = 123,
                Name = "Cardano",
                Ticker = "ADA",
                Price = 222,
                PercentChange = 12
            };
            (bool result, string message) = await service.AddAsset(model);
            Assert.AreEqual("This asset already exists on the plattform", message);
        }
        [Test]
        public async Task AddAssetShouldReturnTrueWhenAssetDoentExist()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var model = new CryptoResponseModel()
            {
                CirculatingSupply = 123,
                Description = "123",
                Logo = "123",
                MarketCap = 123,
                Name = "Bitcoin",
                Ticker = "BTC",
                Price = 222,
                PercentChange = 12
            };
            (bool result, string message) = await service.AddAsset(model);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task GetAllAssetTickersShouldReturnCorrectCount()
        {
            var service = serviceProvider.GetService<IAssetService>();


            var result = await service.GetAllAssetTickers();
            Assert.AreEqual(2, result.Count);
        }
        [Test]
        public async Task GetUserMoneyShouldReturnCorrectAmount()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = await service.GetUserMoney("ivan@abv.bg");
            Assert.AreEqual(10, result);
        }
        [Test]
        public async Task GetAllTickersShouldReturnCorrectTickers()
        {
            var service = serviceProvider.GetService<IAssetService>();


            var result = await service.GetAllAssetTickers();
            Assert.AreEqual(2, result.Count);
        }
        [Test]
        public async Task GetDetailsShouldReturnCorrectDetails()
        {
            var service = serviceProvider.GetService<IAssetService>();


            var result = await service.GetDetails("Cardano");
            Assert.AreEqual("cardanoDescription", result.Description);
        }

        [Test]
        public async Task GetIdsShouldReturnAllIds()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = await service.GetIds();
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task RemoveAssetShouldReturnCorrectWhenAssetIsCorrect()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = await service.RemoveAsset("Cardano");
            Assert.AreEqual(true, result);
        }
        [Test]
        public async Task RemoveAssetShouldReturnFalseWhenAssetIsInCorrect()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = await service.RemoveAsset("Ivan");
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task GetUserFavoritesShouldReturnCorrectCount()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = await service.GetAllFavoritesTickers("ivan@abv.bg");
            Assert.AreEqual("ETH", result[0]);
        }

        [Test]
        public async Task IsAssetFavoriteShouldReturnCorrectWhenAssetIsInFavorite()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = service.IsAssetFavorite("ivan@abv.bg", "ETH");
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task IsAssetFavoriteShouldReturnFalseWhenAssetIsNotFavorite()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = service.IsAssetFavorite("ivan@abv.bg", "ADA");
            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task IsAssetOwnedShouldReturnFalseIfNotOwned()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = await service.IsAssetOwned("Cardano");
            Assert.AreEqual(false, result);
        }
        [Test]
        public async Task IsAssetOwnedShouldReturnTrueIfOwned()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = await service.IsAssetOwned("Ethereum");
            Assert.AreEqual(true, result);
        }
        [Test]
        public async Task UserAssetsShouldReturnCorrectAssets()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = await service.GetUserAssets("ivan@abv.bg");
            Assert.AreEqual("Ethereum", result.Assets.First().AssetName);
        }

        [Test]
        public async Task GetUserAssetsShouldReturnCorrectMoney()
        {
            var service = serviceProvider.GetService<IAssetService>();

            var result = await service.GetUserAssets("ivan@abv.bg");
            Assert.AreEqual(10, result.UserMoney);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        public async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            var crypto = new Asset()
            {
                CirculatingSupply = 123,
                Description = "cardanoDescription",
                Name = "Cardano",
                Ticker = "ADA",
                Id = "1",
                ImageURL = "123.com"
            };

            var ethereum = new Asset()
            {
                CirculatingSupply = 123,
                Description = "ethereumDescription",
                Name = "Ethereum",
                Ticker = "ETH",
                Id = "2",
                ImageURL = "1234.com"
            };

            var user = new ApplicationUser()
            {
                Email = "ivan@abv.bg",
                UserName = "ivan@abv.bg",
                Money = 10,
                Id = "1"
            };

            var userFavorite = new UserFovorites()
            {
                ApplicationUserId = "1",
                AssetId = "2",
            };

            var userAsset = new UserAsset()
            {
                ApplicationUserId = "1",
                AssetId = "2",
                Quantity = 10,
            };

            await repo.AddAsync(userAsset);
            await repo.AddAsync(userFavorite);
            await repo.AddAsync(user);
            await repo.AddAsync(ethereum);
            await repo.AddAsync(crypto);
            await repo.SaveChangesAsync();
        }
    }
}
