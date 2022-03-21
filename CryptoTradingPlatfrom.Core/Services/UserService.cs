using CryptoTradingPlatform.Core.Models.Users;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Infrastructure.Data.Models;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;


        public UserService(ApplicationDbContext _data) 
        {
            data = _data;
        }

        public async Task<bool> IsApplicationSent(string? name)
        {
            return data.ManagerApplications.FirstOrDefault(c => c.User.UserName == name) != null;
        }


        public async Task<(bool, string)> SendManagerApplication(string? name, AddManagerFormModel model)
        {
            var user = data.Users.FirstOrDefault(c => c.UserName == name);
            bool success = false;

            if (data.ManagerApplications.Any(c => c.ApplicationUserId == user.Id))
            {
                return (success, "Your application is currently in progress!");
            }

            ManagerApplication application = new ManagerApplication
            {
                ApplicationUserId = user.Id,
                Experience = model.Experience,
                Question = model.Question,
                DateApplied = DateTime.Now
            };

            try
            {
                await data.ManagerApplications.AddAsync(application);
                await data.SaveChangesAsync();
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
