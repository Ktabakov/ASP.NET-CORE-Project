using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatform.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserAsset>()
                .HasKey(c => new { c.AssetId, c.ApplicationUserId });

            builder.Entity<UserFovorites>()
                .HasKey(c => new {c.ApplicationUserId, c.AssetId});
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<UserAsset> UserAssets { get; set; }

        public DbSet<UserFovorites> UserFavorites { get; set; }

        public DbSet<ManagerApplication> ManagerApplications { get; set; }

        public DbSet<Treasury> Treasury { get; set; }
    }
}