using CryptoTradingPlatform.Data.Models;
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
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder
                .Entity<UserArticle>()
                .HasKey(t => new { t.ArticleId, t.UserId });

            builder
                .Entity<UserAsset>()
                .HasKey(t => new { t.AssetId, t.UserId });

        }


        public DbSet<Asset> Assets { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }      
        //public DbSet<User> Users { get; set; }
        public DbSet<UserAsset> UserAssets { get; set; }
        public DbSet<UserArticle> UserArticles { get; set; }
    }
}