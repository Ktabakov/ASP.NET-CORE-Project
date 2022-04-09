using CryptoTradingPlatform.Core.Models.Users;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Infrastructure.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data.Repositories;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicatioDbRepository repo;

        public UserService(IApplicatioDbRepository _repo) 
        {
            repo = _repo;
        }

        public async Task<bool> IsApplicationSent(string? name)
        {
            var app = repo.All<ManagerApplication>().FirstOrDefault(c => c.User.UserName == name);
            if (app == null)
            {
                return false;
            }
            if (app.Status == "Pending")
            {
                return true;
            }
            return false;
        }


        public async Task<(bool, string)> SendManagerApplication(string? name, AddManagerFormModel model)
        {
            var user = repo.All<ApplicationUser>().FirstOrDefault(c => c.UserName == name);
            bool success = false;

            if (repo.All<ManagerApplication>().Any(c => c.ApplicationUserId == user.Id))
            {
                return (success, "Your application is currently in progress!");
            }

            ManagerApplication application = new ManagerApplication
            {
                ApplicationUserId = user.Id,
                Experience = model.Experience,
                Question = model.Question,
                DateApplied = DateTime.Now,
                Status = "Pending"
            };

            try
            {
                await repo.AddAsync<ManagerApplication>(application);
                await repo.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {
                return (success, "Unexpected Error");
            }

            return (success, null);
        }
    }
}
