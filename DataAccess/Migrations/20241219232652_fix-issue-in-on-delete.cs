using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixissueinondelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirLineFlights_Airlines_AirlineId",
                table: "AirLineFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_AirLineFlights_Flights_FlightId",
                table: "AirLineFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_Airlines_Airports_AirPortArriveId",
                table: "Airlines");

            migrationBuilder.DropForeignKey(
                name: "FK_Airlines_Airports_AirportLeaveId",
                table: "Airlines");

            migrationBuilder.AddForeignKey(
                name: "FK_AirLineFlights_Airlines_AirlineId",
                table: "AirLineFlights",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "AirlineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AirLineFlights_Flights_FlightId",
                table: "AirLineFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Airlines_Airports_AirPortArriveId",
                table: "Airlines",
                column: "AirPortArriveId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Airlines_Airports_AirportLeaveId",
                table: "Airlines",
                column: "AirportLeaveId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirLineFlights_Airlines_AirlineId",
                table: "AirLineFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_AirLineFlights_Flights_FlightId",
                table: "AirLineFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_Airlines_Airports_AirPortArriveId",
                table: "Airlines");

            migrationBuilder.DropForeignKey(
                name: "FK_Airlines_Airports_AirportLeaveId",
                table: "Airlines");

            migrationBuilder.AddForeignKey(
                name: "FK_AirLineFlights_Airlines_AirlineId",
                table: "AirLineFlights",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "AirlineId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirLineFlights_Flights_FlightId",
                table: "AirLineFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Airlines_Airports_AirPortArriveId",
                table: "Airlines",
                column: "AirPortArriveId",
                principalTable: "Airports",
                principalColumn: "AirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Airlines_Airports_AirportLeaveId",
                table: "Airlines",
                column: "AirportLeaveId",
                principalTable: "Airports",
                principalColumn: "AirportId");
        }
    }
}
