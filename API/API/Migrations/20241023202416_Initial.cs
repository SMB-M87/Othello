﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    First = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    { "bad", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(372), "[{\"Type\":0,\"Username\":\"burton\",\"Date\":\"2024-10-23T20:24:16.6920373Z\"},{\"Type\":0,\"Username\":\"bad\",\"Date\":\"2024-10-23T20:24:16.6920374Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-23T20:24:16.6920374Z\"}]", "Badbux" },
                    { "burst", "[\"Cena\",\"Karen\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(360), "[{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-23T20:24:16.6920361Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-23T20:24:16.6920362Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-23T20:24:16.6920362Z\"}]", "Burst" },
                    { "burton", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(363), "[{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-23T20:24:16.6920365Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-23T20:24:16.6920366Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-23T20:24:16.6920366Z\"}]", "Burton" },
                    { "cena", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(356), "[{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-23T20:24:16.6920357Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-23T20:24:16.6920358Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-23T20:24:16.6920359Z\"}]", "Cena" },
                    { "eltjo", "[\"Tijn\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(309), "[{\"Type\":0,\"Username\":\"karen\",\"Date\":\"2024-10-23T20:24:16.692031Z\"},{\"Type\":0,\"Username\":\"ernst\",\"Date\":\"2024-10-23T20:24:16.6920311Z\"},{\"Type\":0,\"Username\":\"john\",\"Date\":\"2024-10-23T20:24:16.6920311Z\"}]", "Eltjo" },
                    { "ernst", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(301), "[{\"Type\":0,\"Username\":\"burton\",\"Date\":\"2024-10-23T20:24:16.6920303Z\"},{\"Type\":0,\"Username\":\"burst\",\"Date\":\"2024-10-23T20:24:16.6920304Z\"},{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-23T20:24:16.6920304Z\"}]", "Ernst" },
                    { "john", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(305), "[{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-23T20:24:16.6920306Z\"},{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-23T20:24:16.6920307Z\"},{\"Type\":0,\"Username\":\"john\",\"Date\":\"2024-10-23T20:24:16.6920308Z\"}]", "John" },
                    { "karen", "[\"Ernst\",\"John\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(291), "[{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-23T20:24:16.6920296Z\"},{\"Type\":0,\"Username\":\"eltjo\",\"Date\":\"2024-10-23T20:24:16.6920299Z\"},{\"Type\":0,\"Username\":\"bad\",\"Date\":\"2024-10-23T20:24:16.69203Z\"}]", "Karen" },
                    { "nice", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(367), "[{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-23T20:24:16.6920368Z\"},{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-23T20:24:16.6920369Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-23T20:24:16.6920371Z\"}]", "Sadbux" },
                    { "tijn", "[\"Eltjo\"]", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(312), "[{\"Type\":0,\"Username\":\"tijn\",\"Date\":\"2024-10-23T20:24:16.6920314Z\"},{\"Type\":0,\"Username\":\"bad\",\"Date\":\"2024-10-23T20:24:16.6920315Z\"},{\"Type\":0,\"Username\":\"sad\",\"Date\":\"2024-10-23T20:24:16.6920315Z\"}]", "Tijn" }
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
                    { "-0", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(615), false, "eltjo", "tijn" },
                    { "-1", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(614), false, "eltjo", "tijn" },
                    { "-10", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(619), false, "john", "eltjo" },
                    { "-11", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(620), false, "ernst", "john" },
                    { "-12", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(620), false, "john", "cena" },
                    { "-13", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(621), false, "tijn", "burst" },
                    { "-14", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(621), false, "karen", "tijn" },
                    { "-15", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(622), false, "ernst", "burton" },
                    { "-16", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(622), false, "john", "burton" },
                    { "-17", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(623), false, "ernst", "cena" },
                    { "-18", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(623), false, "karen", "eltjo" },
                    { "-19", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(624), false, "cena", "burst" },
                    { "-2", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(614), false, "tijn", "eltjo" },
                    { "-20", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(624), false, "burton", "ernst" },
                    { "-21", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(625), false, "john", "tijn" },
                    { "-22", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(625), false, "eltjo", "karen" },
                    { "-23", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(626), false, "burst", "john" },
                    { "-24", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(626), false, "tijn", "cena" },
                    { "-25", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(627), false, "burton", "karen" },
                    { "-26", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(627), false, "eltjo", "burst" },
                    { "-3", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(615), false, "ernst", "karen" },
                    { "-4", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(616), false, "karen", "john" },
                    { "-5", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(616), false, "john", "ernst" },
                    { "-6", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(617), false, "karen", "cena" },
                    { "-7", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(618), false, "burst", "karen" },
                    { "-8", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(618), false, "cena", "ernst" },
                    { "-9", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(619), false, "eltjo", "tijn" },
                    { "0", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(613), false, "eltjo", "tijn" },
                    { "1", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(613), false, "eltjo", "tijn" },
                    { "2", new DateTime(2024, 10, 23, 20, 24, 16, 692, DateTimeKind.Utc).AddTicks(612), false, "tijn", "eltjo" }
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