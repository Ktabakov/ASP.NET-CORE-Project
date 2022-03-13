using CryptoTradingPlatform.Models.Trading;
using CryptoTradingPlatfrom.Core.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface ITradingService
    {
        bool SaveTransaction(TradingFormModel model, string userName);
        Task<bool> SaveSwap (BuyAssetFormModel model, string name);
    }
}
