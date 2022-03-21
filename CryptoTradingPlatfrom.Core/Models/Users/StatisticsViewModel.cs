using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatfrom.Core.Models.Users
{
    public class StatisticsViewModel
    {
        public int TotalTrades { get; set; }

        public string MostTradedAsset { get; set; }

        public int MostTradedAssetTimesTraded { get; set; }

        public decimal TotalFees { get; set; }

        public decimal TradedVolume { get; set; }
    }
}
