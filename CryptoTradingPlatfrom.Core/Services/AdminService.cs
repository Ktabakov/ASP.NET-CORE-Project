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
    public class AdminService : IAdminService
    {
        private readonly IApplicatioDbRepository repo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminService(IApplicatioDbRepository _repo, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            repo = _repo;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public async Task<(bool, string)> ChangeRole(string role, string userId)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(userId);
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
                await repo.SaveChangesAsync();
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

            var application = await repo.GetByIdAsync<ManagerApplication>(id);

            try
            {
                repo.Delete<ManagerApplication>(application);
                await repo.SaveChangesAsync();
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
            return await repo.All<ManagerApplication>()
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
            return await roleManager.Roles.Select(c => c.Name).ToListAsync();
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var result = await repo.All<ApplicationUser>()
                .Where(c => c.UserName != "admin@abv.bg")
                .Select(c => new UserViewModel
                {
                    Username = c.UserName,
                    UserId = c.Id,
                    //Role = roleManager.Roles.FirstOrDefault(r => r.Id == roleManager.Roles.FirstOrDefault(u => u.Id == c.Id).Name).Name,
                    TotalTrades = repo.All<Transaction>().Where(u => u.ApplicationUser.UserName == c.UserName).Count()
                })
                .ToListAsync();


            for (int i = 0; i < result.Count; i++)
            {
                var user = result[i];
                var realUser = await repo.GetByIdAsync<ApplicationUser>(user.UserId);
                user.Role = string.Join(", ", await userManager.GetRolesAsync(realUser));
            }

            return result;
        }

        public async Task<StatisticsViewModel> GetStatistics()
        {
            StatisticsViewModel model = new StatisticsViewModel();
            model.TotalFees = repo.All<Treasury>().FirstOrDefault().Total;
            model.TotalTrades = repo.All<Transaction>().Count();
            model.TradedVolume = repo.All<Transaction>().Select(c => Convert.ToDecimal(c.Quantity) * c.Price).Sum();
            var mostTraded = repo.All<Transaction>()
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
            var user = await repo.GetByIdAsync<ApplicationUser>(id);
            bool success = false;

            var application = repo.GetByIdAsync<ManagerApplication>(id);

            try
            {
                await userManager.AddToRoleAsync(user, "Manager");
                await repo.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {
                return success;
            }

            try
            {
                await repo.DeleteAsync<ManagerApplication>(application.Id);
                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return success;

        }
    }
}
