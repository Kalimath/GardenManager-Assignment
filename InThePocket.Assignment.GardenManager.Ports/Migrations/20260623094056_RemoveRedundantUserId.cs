using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InThePocket.Assignment.GardenManager.Ports.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRedundantUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f"),
                column: "ConcurrencyStamp",
                value: "4af0542c-3bc5-40ba-81e8-5065e2aff0f1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f"),
                columns: new[] { "ConcurrencyStamp", "UserId" },
                values: new object[] { "a13c3842-e4e4-443e-8c6a-1bc74d90debf", new Guid("d4e2f8a9-3c7b-4b8e-9f1a-2a6d8e7c5b3a") });
        }
    }
}
