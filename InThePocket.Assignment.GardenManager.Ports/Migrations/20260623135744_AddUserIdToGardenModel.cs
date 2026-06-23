using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InThePocket.Assignment.GardenManager.Ports.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToGardenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f"),
                column: "ConcurrencyStamp",
                value: "4e9a8533-fc86-4d49-b6d4-86ada3d9d7ee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f"),
                column: "ConcurrencyStamp",
                value: "4af0542c-3bc5-40ba-81e8-5065e2aff0f1");
        }
    }
}
