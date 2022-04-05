using CryptoTradingPlatform.Infrastructure.Data.Common;

namespace CryptoTradingPlatform.Infrastructure.Data.Repositories
{
    public class ApplicatioDbRepository : Repository, IApplicatioDbRepository
    {
        public ApplicatioDbRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}
