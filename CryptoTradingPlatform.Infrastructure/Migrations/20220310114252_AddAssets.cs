using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTradingPlatform.Data.Migrations
{
    public partial class AddAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CirculatingSupply", "CreatorName", "Description", "ImageURL", "Name", "Ticker" },
                values: new object[] { "1", 18978012L, "Satoshi Nakamoto", "Bitcoin (BTC) is a cryptocurrency . Users are able to generate BTC through the process of mining.", "https://s2.coinmarketcap.com/static/img/coins/64x64/1.png", "Bitcoin", "BTC" });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CirculatingSupply", "CreatorName", "Description", "ImageURL", "Name", "Ticker" },
                values: new object[] { "2", 987579314L, "Vitalik Buterin", "Ethereum (ETH) is a cryptocurrency . Users are able to generate ETH through the process of mining.", "https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png", "Ethereum", "ETH" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
