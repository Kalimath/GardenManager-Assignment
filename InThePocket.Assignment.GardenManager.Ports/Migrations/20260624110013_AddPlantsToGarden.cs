using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InThePocket.Assignment.GardenManager.Ports.Migrations
{
    /// <inheritdoc />
    public partial class AddPlantsToGarden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlantType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlantationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurfaceAreaRequired = table.Column<double>(type: "float", nullable: false),
                    IdealHumidityLevel = table.Column<int>(type: "int", nullable: false),
                    GardenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.PlantId);
                    table.ForeignKey(
                        name: "FK_Plants_Gardens_GardenId",
                        column: x => x.GardenId,
                        principalTable: "Gardens",
                        principalColumn: "GardenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealtimePlantMetricData",
                columns: table => new
                {
                    RealtimePlantMetricDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentHumidityLevel = table.Column<double>(type: "float", nullable: false),
                    LastIrrigationStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastIrrigationEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealtimePlantMetricData", x => x.RealtimePlantMetricDataId);
                    table.ForeignKey(
                        name: "FK_RealtimePlantMetricData_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "PlantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f"),
                column: "ConcurrencyStamp",
                value: "0dd06171-eb0a-4dff-b22d-126a48d41bb5");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_GardenId",
                table: "Plants",
                column: "GardenId");

            migrationBuilder.CreateIndex(
                name: "IX_RealtimePlantMetricData_PlantId",
                table: "RealtimePlantMetricData",
                column: "PlantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealtimePlantMetricData");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f"),
                column: "ConcurrencyStamp",
                value: "4e9a8533-fc86-4d49-b6d4-86ada3d9d7ee");
        }
    }
}
