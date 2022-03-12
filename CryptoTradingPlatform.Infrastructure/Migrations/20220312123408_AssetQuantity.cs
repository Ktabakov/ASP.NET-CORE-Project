using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTradingPlatform.Data.Migrations
{
    public partial class AssetQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "Assets",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Assets");

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CirculatingSupply", "Description", "ImageURL", "Name", "Ticker" },
                values: new object[] { "1", 18978012L, "Bitcoin (BTC) is a cryptocurrency . Users are able to generate BTC through the process of mining.", "https://s2.coinmarketcap.com/static/img/coins/64x64/1.png", "Bitcoin", "BTC" });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CirculatingSupply", "Description", "ImageURL", "Name", "Ticker" },
                values: new object[] { "2", 987579314L, "Ethereum (ETH) is a cryptocurrency . Users are able to generate ETH through the process of mining.", "https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png", "Ethereum", "ETH" });
        }
    }
}
