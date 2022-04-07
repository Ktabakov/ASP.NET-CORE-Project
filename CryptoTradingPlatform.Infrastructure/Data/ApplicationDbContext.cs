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

            builder.Entity<ArticleLikes>()
                .HasKey(c => new { c.ApplicationUserId, c.ArticleId });

            builder.Entity<Article>()
                .HasOne(c => c.User)
                .WithMany(c => c.Articles)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Transaction>()
                .HasOne(c => c.Asset)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientNoAction);
                
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<UserAsset> UserAssets { get; set; }

        public DbSet<UserFovorites> UserFavorites { get; set; }

        public DbSet<ManagerApplication> ManagerApplications { get; set; }

        public DbSet<Treasury> Treasury { get; set; }

        public DbSet<ArticleLikes> ArticleLikes { get; set; }
    }
}