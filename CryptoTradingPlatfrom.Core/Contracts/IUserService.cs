using CryptoTradingPlatform.Core.Models.Users;
using CryptoTradingPlatfrom.Core.Models.Users;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface IUserService
    {
        Task<(bool, string)> SendManagerApplication(string? name, AddManagerFormModel model);
        Task<List<ManagerApplicationViewModel>> GetAllApplications();
        Task<bool> DeleteManagerApplication(string id);
        Task<bool> PromoteUserToManager(string id);
        Task<bool> IsApplicationSent(string? name);
        Task<List<string>> GetAllRoles();
        Task<List<UserViewModel>> GetAllUsers();
        Task<(bool, string)> ChangeRole(string role, string userId);
    }
}
