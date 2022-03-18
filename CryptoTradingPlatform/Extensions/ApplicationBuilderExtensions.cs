using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatform.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedAssets(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();
        }
        private static void SeedAssets(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            if (data.Assets.Any())
            {
                return;
            }

            data.Assets.AddRange(new[]
            {
                new Asset
                {
                    Name = "Bitcoin",
                    Description = "Bitcoin (BTC) is a cryptocurrency . Users are able to generate BTC through the process of mining. Bitcoin has a current supply of 18,981,675. The last known price of Bitcoin is 38,908.51091206 USD and is down -0.22 over the last 24 hours. It is currently trading on 9232 active market(s) with $14,683,372,812.30 traded over the last 24 hours. More information can be found at https://bitcoin.org/.",
                    Ticker = "BTC",
                    ImageURL = "https://s2.coinmarketcap.com/static/img/coins/64x64/1.png",
                    CirculatingSupply = 18981675
                },
                new Asset
                {
                    Name="Ethereum",
                    CirculatingSupply = 119974207,
                    Description = "Ethereum (ETH) is a cryptocurrency . Users are able to generate ETH through the process of mining. Ethereum has a current supply of 119,974,206.999. The last known price of Ethereum is 2,614.55697892 USD and is up 3.21 over the last 24 hours. It is currently trading on 5562 active market(s) with $13,221,915,334.49 traded over the last 24 hours. More information can be found at https://www.ethereum.org/.",
                    Ticker = "ETH",
                    ImageURL = "https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png"
                },
                new Asset
                {
                    Name = "Cardano",
                    Ticker = "ADA",
                    CirculatingSupply = 33687391884,
                    Description = "Cardano (ADA) is a cryptocurrency launched in 2017. Users are able to generate ADA through the process of mining. Cardano has a current supply of 34,201,243,285.461 with 33,687,391,884.263 in circulation. The last known price of Cardano is 0.78930052 USD and is down -0.64 over the last 24 hours. It is currently trading on 373 active market(s) with $449,210,953.48 traded over the last 24 hours. More information can be found at https://www.cardano.org.",
                    ImageURL = "https://s2.coinmarketcap.com/static/img/coins/64x64/2010.png"
                }
            });

            data.SaveChanges();
        }
        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Admin"))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Admin" };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@abv.bg";
                    const string adminPassword = "admin123";

                    var user = new ApplicationUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

    }
}
