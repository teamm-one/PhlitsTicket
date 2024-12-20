using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixissue2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Flights_FlightID",
                table: "Seats");

            migrationBuilder.RenameColumn(
                name: "FlightID",
                table: "Seats",
                newName: "FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_FlightID",
                table: "Seats",
                newName: "IX_Seats_FlightId");

            migrationBuilder.AddColumn<int>(
                name: "FlightID",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_FlightID",
                table: "Seats",
                column: "FlightID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Flights_FlightID",
                table: "Seats",
                column: "FlightID",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Flights_FlightId",
                table: "Seats",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Flights_FlightID",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Flights_FlightId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_FlightID",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "FlightID",
                table: "Seats");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Seats",
                newName: "FlightID");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_FlightId",
                table: "Seats",
                newName: "IX_Seats_FlightID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Flights_FlightID",
                table: "Seats",
                column: "FlightID",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
