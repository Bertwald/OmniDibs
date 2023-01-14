using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmniDibs.Migrations
{
    public partial class AirCraftBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserName",
                table: "Accounts",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserName",
                table: "Accounts");
        }
    }
}
