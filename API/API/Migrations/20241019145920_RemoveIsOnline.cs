using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsOnline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Players");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Eltjo",
                columns: new[] { "IsOnline", "LastActivity" },
                values: new object[] { true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Ernst",
                columns: new[] { "IsOnline", "LastActivity" },
                values: new object[] { true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "John",
                columns: new[] { "IsOnline", "LastActivity" },
                values: new object[] { true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Karen",
                columns: new[] { "IsOnline", "LastActivity" },
                values: new object[] { true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Username",
                keyValue: "Tijn",
                columns: new[] { "IsOnline", "LastActivity" },
                values: new object[] { true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
