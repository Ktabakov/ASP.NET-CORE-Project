//using CryptoTradingPlatform.Core.Contracts;
//using CryptoTradingPlatform.Core.Services;
//using CryptoTradingPlatform.Infrastructure.Data.Repositories;
//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace CryptoTradingPlatform.Test.Services
//{
//    public class CryptoApiServiceTest
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
//                .AddSingleton<ICryptoApiService, CryptoApiService>()
//                .BuildServiceProvider();

//            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
//            await SeedDbAsync(repo);
//        }

//        [Test]
//        public async Task ManagerApplicationExistsShouldThrow()
//        {

//            var service = serviceProvider.GetService<ICryptoApiService>();
//            var result = await service.GetCryptos(new List<string> {"BTC", "ADA"});
//            Assert.AreEqual(2, result.Count);
//        }


//        [TearDown]
//        public void TearDown()
//        {
//            dbContext.Dispose();
//        }

//        public async Task SeedDbAsync(IApplicatioDbRepository repo)
//        {
           
//        }
//    }
//}
