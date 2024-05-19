using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RequestTrackerApp.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "RequestNumber", "ClosedDate", "RequestClosedBy", "RequestDate", "RequestMessage", "RequestRaisedBy", "RequestStatus" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2024, 5, 19, 22, 37, 25, 743, DateTimeKind.Local).AddTicks(7674), "Initial request by system", 6, "active" },
                    { 2, null, null, new DateTime(2024, 5, 19, 22, 37, 25, 743, DateTimeKind.Local).AddTicks(7676), "Second request by system", 1, "active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "RequestNumber",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Requests",
                keyColumn: "RequestNumber",
                keyValue: 2);
        }
    }
}
