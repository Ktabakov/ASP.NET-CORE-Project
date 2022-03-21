using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminService(ApplicationDbContext _data, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            data = _data;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public async Task<(bool, string)> ChangeRole(string role, string userId)
        {
            var user = data.Users.FirstOrDefault(u => u.Id == userId);
            bool isInRole = await userManager.IsInRoleAsync(user, role);
            var userRole = await userManager.GetRolesAsync(user);

            string error = string.Empty;
            bool success = false;


            if (role == "User" && userRole.Count == 0)
            {
                return (success, "User is already in that role!");
            }

            if (!await roleManager.RoleExistsAsync(role) && userRole.Count == 0)
            {
                return (success, "Please choose a valid Role");
            }


            if (isInRole)
            {
                return (success, "User is already in that role!");
            }

            try
            {
                if (userRole.Count != 0)
                {
                    await userManager.RemoveFromRoleAsync(user, userRole[0]);
                }
                if (role != "User")
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                await data.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {
                return (success, "Unexpected Error!");
            }


            return (success, null);
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

        public async Task<List<string>> GetAllRoles()
        {
            return await data.Roles.Select(c => c.Name).ToListAsync();
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            return await data
                .Users
                .Where(c => c.UserName != "admin@abv.bg")
                .Select(c => new UserViewModel
                {
                    Username = c.UserName,
                    UserId = c.Id,
                    Role = data.Roles.FirstOrDefault(r => r.Id == data.UserRoles.FirstOrDefault(u => u.UserId == c.Id).RoleId).Name,
                    TotalTrades = data.Transactions.Where(u => u.ApplicationUser.UserName == c.UserName).Count()
                })
                .ToListAsync();

        }

        public async Task<StatisticsViewModel> GetStatistics()
        {
            StatisticsViewModel model = new StatisticsViewModel();
            model.TotalFees = data.Treasury.FirstOrDefault().Total;
            model.TotalTrades = data.Transactions.Count();
            model.TradedVolume = data.Transactions.Select(c => Convert.ToDecimal(c.Quantity) * c.Price).Sum();
            var mostTraded = data
                .Transactions
                .GroupBy(c => c.Asset.Name)
                .Select(x => new { AssetName = x.Key, TimesTraded = x.Count(a => a.AssetId == a.AssetId) })
                .OrderByDescending(x => x.TimesTraded)
                .Select(x => new { x.AssetName, x.TimesTraded })
                .First();
            model.MostTradedAsset = mostTraded.AssetName;
            model.MostTradedAssetTimesTraded = mostTraded.TimesTraded;
            return model;
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
    }
}
