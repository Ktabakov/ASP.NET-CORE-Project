using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data.Repositories;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CryptoTradingPlatform.Test.Services
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
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task UsersAddArticlesShouldReturnTrue()
        {
            var articleService = serviceProvider.GetService<IArticleService>();

            var article = new AddArticleFormModel()
            {
                Content = "psum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset",
                ImageURL = "https://www.lipsum.com/",
                Title = "Test Article",
            };

            (bool result, string message) = await articleService.AddArticle(article, "user@abv.bg");
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task GetAllArticlesShouldReturnCorectNumber()
        {
            var articleService = serviceProvider.GetService<IArticleService>();

            var articles = await articleService.GetArticles();

            Assert.AreEqual(1, articles.Count);
        }

        [Test]
        public void GetTotalLikesShouldReturnCorrectCount()
        {
            var articleService = serviceProvider.GetService<IArticleService>();

            var likes = articleService.getTotalLikes("1");

            Assert.AreEqual(2, likes);
        }

        [Test]
        public async Task LikeArticleShouldReturnTrueWithCorrectUser()
        {
            var articleService = serviceProvider.GetService<IArticleService>();

            bool result = await articleService.LikeArticle("1", "ivan@abv.bg");

            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task TotalCountShouldDecreaseWhenUserDislike()
        {
            var articleService = serviceProvider.GetService<IArticleService>();

            bool result = await articleService.LikeArticle("1", "ivan@abv.bg");
            int totalLikes = articleService.getTotalLikes("1");
            Assert.AreEqual(1, totalLikes);
        }

        [Test]
        public async Task TotalCountShouldIncreaseWhenUserLike()
        {
            var articleService = serviceProvider.GetService<IArticleService>();

            bool result = await articleService.LikeArticle("1", "maria@abv.bg");
            int totalLikes = articleService.getTotalLikes("1");
            Assert.AreEqual(3, totalLikes);
        }

        [Test]
        public async Task LikeArticleShouldReturnFalseWithNullArticle()
        {
            var articleService = serviceProvider.GetService<IArticleService>();

            bool result = await articleService.LikeArticle(null, "ivan@abv.bg");

            Assert.AreEqual(false, result);
        }

        [Test]
        public async Task RemoveArticleShouldReturnTrueWhenIdIsCorrect()
        {
            var articleService = serviceProvider.GetService<IArticleService>();

            bool result = await articleService.RemoveArticle("1");

            Assert.AreEqual(true, result);
        }
        [Test]
        public async Task RemoveArticleShouldReturnFalseWhenIdIsICorrect()
        {
            var articleService = serviceProvider.GetService<IArticleService>();

            bool result = await articleService.RemoveArticle("14");

            Assert.AreEqual(false, result);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        public async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            var userEmail = "user@abv.bg";
            var userPassword = "user123";
            var user = new ApplicationUser
            {
                Email = userEmail,
                UserName = userEmail,
                EmailConfirmed = true,
                Id = "1"
            };

            var ivan = new ApplicationUser()
            {
                Email = "ivan@abv.bg",
                UserName = "ivan@abv.bg",
                EmailConfirmed = true,
                Id = "2",
            };

            var maria = new ApplicationUser()
            {
                Email = "maria@abv.bg",
                UserName = "maria@abv.bg",
                EmailConfirmed = true,
                Id = "3",
            };

            var article = new Article()
            {
                Content = "sdadasdadasdasdasdasdaasds",
                DateAdded = System.DateTime.Now,
                Id = "1",
                Title = "test article",
                ImageURL = "test.com",
                ApplicationUserId = "1",
                Likes = 2,
            };
            var articleLike = new ArticleLikes()
            {
                ArticleId = "1",
                ApplicationUserId = "1",
            };
            var ivanArticleLike = new ArticleLikes()
            {
                ArticleId = "1",
                ApplicationUserId = "2",
            };

            repo.AddAsync(maria);
            repo.AddAsync(ivan);
            repo.AddAsync(articleLike);
            repo.AddAsync(ivanArticleLike);
            repo.AddAsync(article);
            repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}
