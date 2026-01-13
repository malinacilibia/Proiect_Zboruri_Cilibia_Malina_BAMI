using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_Zboruri_Cilibia_Malina.Migrations
{
    /// <inheritdoc />
    public partial class AddPredictionHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PredictionHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<float>(type: "real", nullable: false),
                    TypeOfTravel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightDistance = table.Column<float>(type: "real", nullable: false),
                    InflightWifiService = table.Column<float>(type: "real", nullable: false),
                    DepartureArrivalTimeConvenient = table.Column<float>(type: "real", nullable: false),
                    EaseOfOnlineBooking = table.Column<float>(type: "real", nullable: false),
                    GateLocation = table.Column<float>(type: "real", nullable: false),
                    FoodAndDrink = table.Column<float>(type: "real", nullable: false),
                    OnlineBoarding = table.Column<float>(type: "real", nullable: false),
                    SeatComfort = table.Column<float>(type: "real", nullable: false),
                    InflightEntertainment = table.Column<float>(type: "real", nullable: false),
                    OnBoardService = table.Column<float>(type: "real", nullable: false),
                    LegRoomService = table.Column<float>(type: "real", nullable: false),
                    BaggageHandling = table.Column<float>(type: "real", nullable: false),
                    CheckinService = table.Column<float>(type: "real", nullable: false),
                    InflightService = table.Column<float>(type: "real", nullable: false),
                    Cleanliness = table.Column<float>(type: "real", nullable: false),
                    DepartureDelayInMinutes = table.Column<float>(type: "real", nullable: false),
                    ArrivalDelayInMinutes = table.Column<float>(type: "real", nullable: false),
                    PredictedSatisfaction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictionHistories", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredictionHistories");
        }
    }
}
