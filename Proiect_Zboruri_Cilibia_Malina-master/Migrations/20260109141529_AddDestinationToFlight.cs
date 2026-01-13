using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_Zboruri_Cilibia_Malina.Migrations
{
    /// <inheritdoc />
    public partial class AddDestinationToFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Flights");
        }
    }
}
