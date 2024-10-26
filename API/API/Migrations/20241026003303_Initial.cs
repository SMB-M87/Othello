using Microsoft.EntityFrameworkCore.Migrations;

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
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    { "badbux", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7086), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-26T00:33:03.4527087Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-26T00:33:03.4527088Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527089Z\"}]", "Badbux" },
                    { "burst", "[\"Cena\",\"Karen\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7074), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527076Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527077Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527078Z\"}]", "Burst" },
                    { "burton", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7079), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.452708Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527081Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527082Z\"}]", "Burton" },
                    { "cena", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7071), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527072Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527073Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527073Z\"}]", "Cena" },
                    { "eltjo", "[\"Tijn\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7063), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-26T00:33:03.4527065Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-26T00:33:03.4527066Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-26T00:33:03.4527066Z\"}]", "Eltjo" },
                    { "ernst", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7010), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-26T00:33:03.4527056Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-26T00:33:03.4527058Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527058Z\"}]", "Ernst" },
                    { "identity", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7090), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-26T00:33:03.4527091Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-26T00:33:03.4527092Z\"},{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-26T00:33:03.4527092Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-26T00:33:03.4527093Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-26T00:33:03.4527093Z\"},{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-26T00:33:03.4527095Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527095Z\"}]", "Identity" },
                    { "john", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7060), "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527061Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527062Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.4527062Z\"}]", "John" },
                    { "karen", "[\"Ernst\",\"John\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7002), "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-26T00:33:03.4527006Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-26T00:33:03.4527008Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-26T00:33:03.4527009Z\"}]", "Karen" },
                    { "sadbux", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7083), "[{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-26T00:33:03.4527084Z\"},{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-26T00:33:03.4527085Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-26T00:33:03.4527085Z\"}]", "Sadbux" },
                    { "tijn", "[\"Eltjo\"]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7067), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-26T00:33:03.4527068Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-26T00:33:03.4527069Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-26T00:33:03.452707Z\"}]", "Tijn" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Token", "Board", "Date", "Description", "FColor", "First", "PlayersTurn", "SColor", "Second", "Status" },
                values: new object[,]
                {
                    { "four", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7255), "I wanna play a game and don't have any requirements!", 2, "burst", 2, 1, null, 0 },
                    { "one", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7242), "I search an advanced player!", 2, "ernst", 2, 1, "john", 1 },
                    { "three", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7251), "I wanna play a game and don't have any requirements!", 2, "cena", 2, 1, null, 0 },
                    { "two", "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7246), "I want to player more than one game against the same adversary.", 2, "eltjo", 1, 1, "tijn", 1 },
                    { "zero", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7237), "I wanna play a game and don't have any requirements!", 2, "karen", 2, 1, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Token", "Date", "Draw", "Loser", "Winner" },
                values: new object[,]
                {
                    { "-0", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7274), false, "eltjo", "tijn" },
                    { "-1", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7274), false, "eltjo", "tijn" },
                    { "-10", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7320), false, "john", "eltjo" },
                    { "-11", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7320), false, "ernst", "john" },
                    { "-12", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7321), false, "john", "cena" },
                    { "-13", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7321), false, "tijn", "burst" },
                    { "-14", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7322), false, "karen", "tijn" },
                    { "-15", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7322), false, "ernst", "burton" },
                    { "-16", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7323), false, "john", "burton" },
                    { "-17", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7323), false, "ernst", "cena" },
                    { "-18", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7323), false, "karen", "eltjo" },
                    { "-19", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7324), false, "cena", "burst" },
                    { "-2", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7273), false, "tijn", "eltjo" },
                    { "-20", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7324), false, "burton", "ernst" },
                    { "-21", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7325), false, "john", "tijn" },
                    { "-22", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7325), false, "eltjo", "karen" },
                    { "-23", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7326), false, "burst", "john" },
                    { "-24", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7326), false, "tijn", "cena" },
                    { "-25", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7327), false, "burton", "karen" },
                    { "-26", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7327), false, "eltjo", "burst" },
                    { "-3", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7275), false, "ernst", "karen" },
                    { "-4", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7316), false, "karen", "john" },
                    { "-5", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7317), false, "john", "ernst" },
                    { "-6", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7317), false, "karen", "cena" },
                    { "-7", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7318), false, "burst", "karen" },
                    { "-8", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7319), false, "cena", "ernst" },
                    { "-9", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7319), false, "eltjo", "tijn" },
                    { "0", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7273), false, "eltjo", "tijn" },
                    { "1", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7272), false, "eltjo", "tijn" },
                    { "2", new DateTime(2024, 10, 26, 0, 33, 3, 452, DateTimeKind.Utc).AddTicks(7271), false, "tijn", "eltjo" }
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
