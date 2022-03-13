using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Models.Trading;
using CryptoTradingPlatfrom.Core.Contracts;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class TradingService : ITradingService
    {
        private readonly ApplicationDbContext data;
        public TradingService(ApplicationDbContext _data)
        {
            data = _data;
        }
        public bool SaveTransaction(TradingFormModel model)
        {
            /*decimal sum = model.Price * model.Quantity;
            if ()*/
            throw new NotImplementedException();
        }
    }
}
