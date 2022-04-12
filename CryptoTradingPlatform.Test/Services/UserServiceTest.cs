using CryptoTradingPlatform.Core.Models.Users;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data.Repositories;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CryptoTradingPlatform.Test.Services
{
    public class UserServiceTest
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
        public async Task ManagerApplicationExistsShouldThrow()
        {
            var managerApplication = new AddManagerFormModel()
            {
                Question = "Test Question Answer",
                Experience = "The best Experience",
            };
            var service = serviceProvider.GetService<IUserService>();
            (bool result, string message) = await service.SendManagerApplication("Joro", managerApplication);
            Assert.AreEqual("Your application is currently in progress!", message);
        }

        [Test]
        public async Task ManagerApplicationSavedShouldReturnTrue()
        {
            var managerApplication = new AddManagerFormModel()
            {
                Question = "Test Question Second Answer",
                Experience = "The best Experience second test",
            };
            var service = serviceProvider.GetService<IUserService>();
            (bool result, string message) = await service.SendManagerApplication("Misho", managerApplication);
            Assert.AreEqual(true, result);
        }
        [Test]
        public async Task IsApplicationSentShouldReturnCorrect()
        {           
            var service = serviceProvider.GetService<IUserService>();
            bool result = await service.IsApplicationSent("Joro");
            Assert.AreEqual(true, result);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        public async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            var user = new ApplicationUser()
            {
                Id = "1",
                UserName = "Joro"
            };
            var user2 = new ApplicationUser()
            {
                Id = "2",
                UserName = "Misho"
            };

            var managerApplication = new ManagerApplication()
            {
                Question = "The Best Answer Ever",
                Experience = "1 - 5 Years",          
                Id = "1",
                User = user,
                Status = "Pending"
            };

            await repo.AddAsync(user2);
            await repo.AddAsync(managerApplication);
            await repo.SaveChangesAsync();
        }
    }
}