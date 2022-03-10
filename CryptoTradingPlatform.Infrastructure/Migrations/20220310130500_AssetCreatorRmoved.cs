using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTradingPlatform.Data.Migrations
{
    public partial class AssetCreatorRmoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Assets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatorName",
                value: "Satoshi Nakamoto");

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatorName",
                value: "Vitalik Buterin");
        }
    }
}
