using CryptoTradingPlatform.Controllers;
using CryptoTradingPlatform.Core.Models.Users;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data.Repositories;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CryptoTradingPlatform.Test.Controllers
{
    public class UsersControllerTest
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
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            await SeedDbAsync(repo);
        }


        [Test]
        public void CreateShouldReturnView()
        {
            var service = serviceProvider.GetService<IUserService>();
            var usersController = new UsersController(service);

            var result = usersController.Create();

            Assert.NotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }
        
        [Test]
        public async Task WrongModelShouldReturnView()
        {
            var model = new AddManagerFormModel()
            {
                Experience = "sdsd",
                Question = "23"
            };

            var service = serviceProvider.GetService<IUserService>();
            var usersController = new UsersController(service);

            var result = usersController.Create(model);

            Assert.NotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        public async Task SeedDbAsync(IApplicatioDbRepository repo)
        {

            ApplicationUser user = new ApplicationUser
            {
                Id = "1",
                UserName = "user@abv.bg",
                Email = "user@abv.bg",
            };

            await repo.AddAsync(user);

        }
    }
}
