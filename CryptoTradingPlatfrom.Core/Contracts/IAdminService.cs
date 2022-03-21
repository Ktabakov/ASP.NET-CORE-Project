using CryptoTradingPlatfrom.Core.Models.Users;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface IAdminService
    {
        Task<List<ManagerApplicationViewModel>> GetAllApplications();
        Task<bool> DeleteManagerApplication(string id);
        Task<bool> PromoteUserToManager(string id);
        Task<List<string>> GetAllRoles();
        Task<List<UserViewModel>> GetAllUsers();
        Task<(bool, string)> ChangeRole(string role, string userId);
        Task<StatisticsViewModel> GetStatistics();
    }
}
