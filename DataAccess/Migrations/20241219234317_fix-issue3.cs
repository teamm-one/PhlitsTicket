using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixissue3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirLineFlights_Flights_FlightId",
                table: "AirLineFlights");

            migrationBuilder.AddForeignKey(
                name: "FK_AirLineFlights_Flights_FlightId",
                table: "AirLineFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirLineFlights_Flights_FlightId",
                table: "AirLineFlights");

            migrationBuilder.AddForeignKey(
                name: "FK_AirLineFlights_Flights_FlightId",
                table: "AirLineFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId");
        }
    }
}
