﻿//using CryptoTradingPlatform.Data.Models;
//using CryptoTradingPlatform.Infrastructure.Data.Repositories;
//using CryptoTradingPlatfrom.Core.Contracts;
//using CryptoTradingPlatfrom.Core.Models.Users;
//using CryptoTradingPlatfrom.Core.Services;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CryptoTradingPlatform.Test
//{
//    public class AdminServiceTest
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
//                .AddSingleton<IAdminService, AdminService>()
//                .BuildServiceProvider();

//            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
//            await SeedDbAsync(repo);
//        }

//        [Test]
//        public async Task GetAllUsersShouldReturnCorrectCount()
//        {
//            var adminService = serviceProvider.GetService<IAdminService>();


//            List<UserViewModel> users = await adminService.GetAllUsers();
//            Assert.AreEqual(1, users.Count);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            dbContext.Dispose();
//        }

//        public async Task SeedDbAsync(IApplicatioDbRepository repo)
//        {
//            var user = new ApplicationUser()
//            {
//                Id = "1",
//                UserName = "Joro"
//            };

//            await repo.AddAsync(user);
//            await repo.SaveChangesAsync();
//        }
//    }
//}