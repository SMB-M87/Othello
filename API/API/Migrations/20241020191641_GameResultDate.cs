using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class GameResultDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Results",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Token",
                keyValue: "zero",
                columns: new[] { "FColor", "SColor" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Eltjo",
                column: "LastActivity",
                value: new DateTime(2024, 10, 20, 19, 16, 41, 711, DateTimeKind.Utc).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Ernst",
                column: "LastActivity",
                value: new DateTime(2024, 10, 20, 19, 16, 41, 711, DateTimeKind.Utc).AddTicks(2817));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "John",
                column: "LastActivity",
                value: new DateTime(2024, 10, 20, 19, 16, 41, 711, DateTimeKind.Utc).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Karen",
                column: "LastActivity",
                value: new DateTime(2024, 10, 20, 19, 16, 41, 711, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Tijn",
                column: "LastActivity",
                value: new DateTime(2024, 10, 20, 19, 16, 41, 711, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "Token",
                keyValue: "-0",
                column: "Date",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "Token",
                keyValue: "-1",
                column: "Date",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Results",
                keyColumn: "Token",
                keyValue: "-2",
                column: "Date",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Results");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Token",
                keyValue: "zero",
                columns: new[] { "FColor", "SColor" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Eltjo",
                column: "LastActivity",
                value: new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6532));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Ernst",
                column: "LastActivity",
                value: new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6528));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "John",
                column: "LastActivity",
                value: new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6530));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Karen",
                column: "LastActivity",
                value: new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6521));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Tijn",
                column: "LastActivity",
                value: new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6534));
        }
    }
}
