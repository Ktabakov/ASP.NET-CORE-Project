using CryptoTradingPlatform.Core.Models.Users;
using CryptoTradingPlatfrom.Core.Models.Users;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface IUserService
    {
        Task<(bool, string)> SendManagerApplication(string? name, AddManagerFormModel model);
        Task<bool> IsApplicationSent(string? name);

    }
}
