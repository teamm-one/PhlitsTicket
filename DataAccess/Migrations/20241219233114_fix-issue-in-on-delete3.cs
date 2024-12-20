using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixissueinondelete3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airlines_Airports_AirPortArriveId",
                table: "Airlines");

            migrationBuilder.AddForeignKey(
                name: "FK_Airlines_Airports_AirPortArriveId",
                table: "Airlines",
                column: "AirPortArriveId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airlines_Airports_AirPortArriveId",
                table: "Airlines");

            migrationBuilder.AddForeignKey(
                name: "FK_Airlines_Airports_AirPortArriveId",
                table: "Airlines",
                column: "AirPortArriveId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
