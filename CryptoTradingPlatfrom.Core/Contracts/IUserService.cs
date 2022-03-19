using CryptoTradingPlatform.Core.Models.Users;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface IUserService
    {
        Task<(bool, string)> SendManagerApplication(string? name, AddManagerFormModel model);
    }
}
