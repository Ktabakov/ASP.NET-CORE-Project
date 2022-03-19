using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTradingPlatform.Data.Migrations
{
    public partial class ManagerApplicationsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManagerApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Question = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagerApplications_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagerApplications_ApplicationUserId",
                table: "ManagerApplications",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagerApplications");
        }
    }
}
