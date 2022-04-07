using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data.Repositories;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CryptoTradingPlatform.Test
{
    public class ArticleServiceTest
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
                .AddSingleton<IArticleService, ArticleService>()
                .AddSingleton<UserManager<ApplicationUser>>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task UsersAddArticlesShouldReturnError()
        {
            var service = serviceProvider.GetService<IArticleService>();
            var article = new AddArticleFormModel()
            {
                Content = "psum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset",
                ImageURL = "https://www.lipsum.com/",
                Title = "Test Article",
            };

            (bool result, string message) = await service.AddArticle(article, "admin@abv.bg");
            Assert.AreEqual("We don't do that here!", message);
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        public async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            const string adminEmail = "admin@abv.bg";
            var user = new ApplicationUser
            {
                Email = adminEmail,
                UserName = adminEmail,
                EmailConfirmed = true,
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}
