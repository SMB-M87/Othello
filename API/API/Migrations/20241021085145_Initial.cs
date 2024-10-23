using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Friends = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requests = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PlayersTurn = table.Column<int>(type: "int", nullable: false),
                    First = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FColor = table.Column<int>(type: "int", nullable: false),
                    Second = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SColor = table.Column<int>(type: "int", nullable: false),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Token);
                    table.ForeignKey(
                        name: "FK_Games_Players_First",
                        column: x => x.First,
                        principalTable: "Players",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Players_Second",
                        column: x => x.Second,
                        principalTable: "Players",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Winner = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Loser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Draw = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Token);
                    table.ForeignKey(
                        name: "FK_Results_Players_Loser",
                        column: x => x.Loser,
                        principalTable: "Players",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Players_Winner",
                        column: x => x.Winner,
                        principalTable: "Players",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Token", "Friends", "LastActivity", "Requests", "Username" },
                values: new object[,]
                {
                    { "bad", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1469), "[{\"Type\":0,\"Username\":\"burton\",\"Date\":\"2024-10-21T08:51:45.588147Z\"},{\"Type\":0,\"Username\":\"bad\",\"Date\":\"2024-10-21T08:51:45.5881471Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881471Z\"}]", "Badbux" },
                    { "burst", "[\"Cena\",\"Karen\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1458), "[{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881459Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.588146Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.588146Z\"}]", "Burst" },
                    { "burton", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1461), "[{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881463Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881464Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881464Z\"}]", "Burton" },
                    { "cena", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1454), "[{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881455Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881456Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881457Z\"}]", "Cena" },
                    { "eltjo", "[\"Tijn\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1447), "[{\"Type\":0,\"Username\":\"karen\",\"Date\":\"2024-10-21T08:51:45.5881448Z\"},{\"Type\":0,\"Username\":\"ernst\",\"Date\":\"2024-10-21T08:51:45.5881449Z\"},{\"Type\":0,\"Username\":\"john\",\"Date\":\"2024-10-21T08:51:45.5881449Z\"}]", "Eltjo" },
                    { "ernst", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1438), "[{\"Type\":0,\"Username\":\"burton\",\"Date\":\"2024-10-21T08:51:45.588144Z\"},{\"Type\":0,\"Username\":\"burst\",\"Date\":\"2024-10-21T08:51:45.5881441Z\"},{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881442Z\"}]", "Ernst" },
                    { "john", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1443), "[{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881444Z\"},{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881445Z\"},{\"Type\":0,\"Username\":\"john\",\"Date\":\"2024-10-21T08:51:45.5881446Z\"}]", "John" },
                    { "karen", "[\"Ernst\",\"John\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1428), "[{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881432Z\"},{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-21T08:51:45.5881436Z\"},{\"Type\":0,\"Username\":\"bad\",\"Date\":\"2024-10-21T08:51:45.5881436Z\"}]", "Karen" },
                    { "nice", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1465), "[{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881467Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881467Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881468Z\"}]", "Sadbux" },
                    { "tijn", "[\"Eltjo\"]", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1450), "[{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-21T08:51:45.5881451Z\"},{\"Type\":0,\"Username\":\"bad\",\"Date\":\"2024-10-21T08:51:45.5881452Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-21T08:51:45.5881453Z\"}]", "Tijn" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Token", "Board", "Description", "FColor", "First", "PlayersTurn", "SColor", "Second", "Status" },
                values: new object[,]
                {
                    { "four", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", "I wanna play a game and don't have any requirements!", 2, "burst", 2, 1, null, 0 },
                    { "one", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", "I search an advanced player!", 2, "ernst", 2, 1, "john", 1 },
                    { "three", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", "I wanna play a game and don't have any requirements!", 2, "cena", 2, 1, null, 0 },
                    { "two", "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", "I want to player more than one game against the same adversary.", 2, "eltjo", 1, 1, "tijn", 1 },
                    { "zero", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", "I wanna play a game and don't have any requirements!", 2, "karen", 2, 1, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Token", "Date", "Draw", "Loser", "Winner" },
                values: new object[,]
                {
                    { "-0", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1705), false, "eltjo", "tijn" },
                    { "-1", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1705), false, "eltjo", "tijn" },
                    { "-10", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1712), false, "john", "eltjo" },
                    { "-11", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1713), false, "ernst", "john" },
                    { "-12", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1713), false, "john", "cena" },
                    { "-13", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1714), false, "tijn", "burst" },
                    { "-14", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1714), false, "karen", "tijn" },
                    { "-15", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1715), false, "ernst", "burton" },
                    { "-16", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1715), false, "john", "burton" },
                    { "-17", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1716), false, "ernst", "cena" },
                    { "-18", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1716), false, "karen", "eltjo" },
                    { "-19", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1717), false, "cena", "burst" },
                    { "-2", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1704), false, "tijn", "eltjo" },
                    { "-20", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1717), false, "burton", "ernst" },
                    { "-21", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1718), false, "john", "tijn" },
                    { "-22", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1718), false, "eltjo", "karen" },
                    { "-23", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1719), false, "burst", "john" },
                    { "-24", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1719), false, "tijn", "cena" },
                    { "-25", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1720), false, "burton", "karen" },
                    { "-26", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1720), false, "eltjo", "burst" },
                    { "-3", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1706), false, "ernst", "karen" },
                    { "-4", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1706), false, "karen", "john" },
                    { "-5", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1707), false, "john", "ernst" },
                    { "-6", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1707), false, "karen", "cena" },
                    { "-7", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1709), false, "burst", "karen" },
                    { "-8", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1709), false, "cena", "ernst" },
                    { "-9", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1710), false, "eltjo", "tijn" },
                    { "0", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1704), false, "eltjo", "tijn" },
                    { "1", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1703), false, "eltjo", "tijn" },
                    { "2", new DateTime(2024, 10, 21, 8, 51, 45, 588, DateTimeKind.Utc).AddTicks(1702), false, "tijn", "eltjo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_First",
                table: "Games",
                column: "First");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Second",
                table: "Games",
                column: "Second");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Username",
                table: "Players",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_Loser",
                table: "Results",
                column: "Loser");

            migrationBuilder.CreateIndex(
                name: "IX_Results_Winner",
                table: "Results",
                column: "Winner");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
