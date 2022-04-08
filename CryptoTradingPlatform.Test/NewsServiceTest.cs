//using Castle.Core.Configuration;
//using CryptoTradingPlatform.Core.Models.Articles;
//using CryptoTradingPlatform.Infrastructure.Data.Repositories;
//using CryptoTradingPlatfrom.Core.Contracts;
//using CryptoTradingPlatfrom.Core.Services;
//using Microsoft.Extensions.DependencyInjection;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace CryptoTradingPlatform.Test
//{
//    public class NewsServiceTest
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
//                .AddSingleton<INewsService, NewsService>()
//                .AddSingleton<HttpClient>()
//                .AddSingleton<IConfiguration>()
//                .BuildServiceProvider();

//            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
//            await SeedDbAsync(repo);
//        }

       
//        [Test]
//        public async Task IsApplicationSentShouldReturnCorrect()
//        {
//            var service = serviceProvider.GetService<INewsService>();
//            List<NewsViewModel> result = await service.GetNews();
//            Assert.AreEqual(true, result);
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
