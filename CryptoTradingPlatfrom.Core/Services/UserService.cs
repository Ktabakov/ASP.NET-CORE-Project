using CryptoTradingPlatform.Core.Models.Users;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Infrastructure.Data.Models;
using CryptoTradingPlatfrom.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext _data)
        {
            data = _data;
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
