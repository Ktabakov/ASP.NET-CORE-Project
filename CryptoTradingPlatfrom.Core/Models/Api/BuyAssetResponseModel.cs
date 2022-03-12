using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatfrom.Core.Models.Api
{
    public class BuyAssetResponseModel
    {
        public string SellAssetTicker { get; set; }

        public decimal SellAssetPrice { get; set; }

        public string BuyAssetTicker { get; set; }

        public decimal BuyAssetPrice { get; set; }
    }
}
