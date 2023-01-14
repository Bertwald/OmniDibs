using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmniDibs.Migrations
{
    public partial class AirCraftBooking2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirPlaneBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AirplaneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirPlaneBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirPlaneBookings_Airplanes_AirplaneId",
                        column: x => x.AirplaneId,
                        principalTable: "Airplanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirPlaneBookings_Bookings_Id",
                        column: x => x.Id,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirPlaneBookings_AirplaneId",
                table: "AirPlaneBookings",
                column: "AirplaneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirPlaneBookings");
        }
    }
}
