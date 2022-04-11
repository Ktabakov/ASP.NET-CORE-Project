//using CryptoTradingPlatform.Core.Contracts;
//using CryptoTradingPlatform.Core.Services;
//using CryptoTradingPlatform.Data.Models;
//using CryptoTradingPlatform.Infrastructure.Data.Repositories;
//using CryptoTradingPlatfrom.Core.Contracts;
//using CryptoTradingPlatfrom.Core.Models.Assets;
//using CryptoTradingPlatfrom.Core.Services;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using Moq.Protected;
//using NUnit.Framework;
//using System.Net;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CryptoTradingPlatform.Test
//{
//    public class TradingServiceTest
//    {
//        private ServiceProvider serviceProvider;
//        private InMemoryDbContext dbContext;


//        [SetUp]
//        public async Task Setup()
//        {

//            dbContext = new InMemoryDbContext();
//            var serviceCollection = new ServiceCollection();

//            serviceProvider = serviceCollection
//                .AddSingleton(sp => dbContext.CreateContext())
//                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
//                .AddSingleton<ITradingService, TradingService>()
//                .AddSingleton<ICryptoApiService, CryptoApiService>()
//                .BuildServiceProvider();

//            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
//            await SeedDbAsync(repo);
//        }

//        [Test]
//        public async Task UsersAddArticlesShouldReturnError()
//        {
//            var tradingService = serviceProvider.GetService<ITradingService>();
//            var cryptoApiService = serviceProvider.GetService<ICryptoApiService>();


//            BuyAssetFormModel model = new BuyAssetFormModel()
//            {
//                SellAssetId = "sell",
//                BuyAssetId = "buy",
//                SellAssetQyantity = 2,
//                BuyAssetQuantity = 3,
//            };

//            var result = await tradingService.CalculateTransaction(model);
//            Assert.AreEqual(2, result);
//        }


//        [TearDown]
//        public void TearDown()
//        {
//            dbContext.Dispose();
//        }

//        public async Task SeedDbAsync(IApplicatioDbRepository repo)
//        {
//            var sellAsset = new Asset()
//            {
//                CirculatingSupply = 14,
//                Description = "dsdasdadadasda",
//                ImageURL = "sdadasdasdas",
//                Name = "sellAsset",
//                Ticker = "sellAsset",
//                Id = "sell"
//            };
//            var buyAsset = new Asset()
//            {
//                CirculatingSupply = 14,
//                Description = "dsdasdadadasda",
//                ImageURL = "sdadasdasdas",
//                Name = "buyAsset",
//                Ticker = "buyAsset",
//                Id = "buy"
//            };

//            await repo.AddAsync<Asset>(sellAsset);
//            await repo.AddAsync<Asset>(buyAsset);
//            await repo.SaveChangesAsync();

//        }
//    }
//}
