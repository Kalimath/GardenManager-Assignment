using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InThePocket.Assignment.GardenManager.Ports.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDemoSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserId", "UserName" },
                values: new object[] { new Guid("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f"), 0, (short)34, "a13c3842-e4e4-443e-8c6a-1bc74d90debf", "john.doe@email.com", false, "John", "Doe", false, null, null, null, null, null, false, null, false, new Guid("d4e2f8a9-3c7b-4b8e-9f1a-2a6d8e7c5b3a"), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b3c1a8f4-5d6e-4a7b-9c2e-8f3d7a6e1c4f"));
        }
    }
}
