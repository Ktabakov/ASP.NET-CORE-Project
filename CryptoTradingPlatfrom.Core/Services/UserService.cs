using CryptoTradingPlatform.Core.Models.Users;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Infrastructure.Data.Models;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(ApplicationDbContext _data, UserManager<ApplicationUser> _userManager)
        {
            data = _data;
            userManager = _userManager;
        }

        public async Task<bool> DeleteManagerApplication(string id)
        {
            bool success = false;

            var application = data
                .ManagerApplications
                .FirstOrDefault(c => c.ApplicationUserId == id);

            try
            {
                data.ManagerApplications.Remove(application);
                await data.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {
                return success;
            }

            return success;
        }

        public async Task<List<ManagerApplicationViewModel>> GetAllApplications()
        {
            return await data
                .ManagerApplications
                .Select(c => new ManagerApplicationViewModel
                {
                    DateApplied = c.DateApplied,
                    Experience = c.Experience,
                    Question = c.Question,
                    Username = c.User.UserName,
                    UserId = c.User.Id
                })
                .ToListAsync();
        }

        public async Task<bool> IsApplicationSent(string? name)
        {
            return data.ManagerApplications.FirstOrDefault(c => c.User.UserName == name) != null;
        }

        public async Task<bool> PromoteUserToManager(string id)
        {
            var user = data.Users.FirstOrDefault(u => u.Id == id);
            bool success = false;

            var application = data
               .ManagerApplications
               .FirstOrDefault(c => c.ApplicationUserId == id);

            try
            {
                await userManager.AddToRoleAsync(user, "Manager");
                data.SaveChanges();
                success = true;
            }
            catch (Exception)
            {
                return success;
            }

            try
            {
                data.ManagerApplications.Remove(application);
                await data.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return success;   

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
