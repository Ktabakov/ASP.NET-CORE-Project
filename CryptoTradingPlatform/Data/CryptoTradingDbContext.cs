using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatform.Data
{
    public class CryptoTradingDbContext : IdentityDbContext
    {
        public CryptoTradingDbContext(DbContextOptions<CryptoTradingDbContext> options)
            : base(options)
        {
        }
    }
}