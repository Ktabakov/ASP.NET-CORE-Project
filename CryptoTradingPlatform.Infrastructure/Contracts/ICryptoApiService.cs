using CryptoTradingPlatform.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatform.Infrastructure.Contracts
{
    public interface ICryptoApiService
    {
        Task<CryptoResponseModel> GetFirst();

        TopCryptosApiRequestModel GetTopFive();
    }
}
