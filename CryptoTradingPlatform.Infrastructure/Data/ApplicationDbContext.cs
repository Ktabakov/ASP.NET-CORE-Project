using CryptoTradingPlatform.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatform.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Asset>().HasData(
                new Asset
                {
                    Name = "Bitcoin",
                    Ticker = "BTC",
                    ImageURL = "https://s2.coinmarketcap.com/static/img/coins/64x64/1.png",
                    Id = "1",
                    Description = "Bitcoin (BTC) is a cryptocurrency . Users are able to generate BTC through the process of mining.",
                    CirculatingSupply = 18978012
                },
                new Asset
                {
                    Name = "Ethereum",
                    Ticker = "ETH",
                    ImageURL = "https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png",
                    Id = "2",
                    Description = "Ethereum (ETH) is a cryptocurrency . Users are able to generate ETH through the process of mining.",
                    CirculatingSupply = 987579314
                }
             );

        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Asset> Assets { get; set; }
    }
}