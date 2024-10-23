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
                    { "badbux", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8300), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T21:17:51.2258301Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T21:17:51.2258302Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258302Z\"}]", "Badbux" },
                    { "burst", "[\"Cena\",\"Karen\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8288), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.225829Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258291Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258291Z\"}]", "Burst" },
                    { "burton", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8292), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258293Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258294Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258295Z\"}]", "Burton" },
                    { "cena", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8284), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258286Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258286Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258287Z\"}]", "Cena" },
                    { "eltjo", "[\"Tijn\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8277), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-23T21:17:51.2258278Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-23T21:17:51.2258279Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-23T21:17:51.225828Z\"}]", "Eltjo" },
                    { "ernst", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8269), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T21:17:51.2258271Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-23T21:17:51.2258271Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258272Z\"}]", "Ernst" },
                    { "identity", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8303), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T21:17:51.2258304Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T21:17:51.2258305Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258306Z\"}]", "Identity" },
                    { "john", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8273), "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258275Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258276Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258276Z\"}]", "John" },
                    { "karen", "[\"Ernst\",\"John\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8261), "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T21:17:51.2258264Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T21:17:51.2258267Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T21:17:51.2258268Z\"}]", "Karen" },
                    { "sadbux", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8296), "[{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-23T21:17:51.2258297Z\"},{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T21:17:51.2258298Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-23T21:17:51.2258299Z\"}]", "Sadbux" },
                    { "tijn", "[\"Eltjo\"]", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8281), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-23T21:17:51.2258282Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T21:17:51.2258283Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T21:17:51.2258283Z\"}]", "Tijn" }
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
                    { "-0", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8533), false, "eltjo", "tijn" },
                    { "-1", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8533), false, "eltjo", "tijn" },
                    { "-10", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8537), false, "john", "eltjo" },
                    { "-11", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8538), false, "ernst", "john" },
                    { "-12", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8538), false, "john", "cena" },
                    { "-13", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8539), false, "tijn", "burst" },
                    { "-14", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8539), false, "karen", "tijn" },
                    { "-15", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8540), false, "ernst", "burton" },
                    { "-16", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8540), false, "john", "burton" },
                    { "-17", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8541), false, "ernst", "cena" },
                    { "-18", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8542), false, "karen", "eltjo" },
                    { "-19", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8542), false, "cena", "burst" },
                    { "-2", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8532), false, "tijn", "eltjo" },
                    { "-20", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8543), false, "burton", "ernst" },
                    { "-21", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8543), false, "john", "tijn" },
                    { "-22", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8544), false, "eltjo", "karen" },
                    { "-23", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8545), false, "burst", "john" },
                    { "-24", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8545), false, "tijn", "cena" },
                    { "-25", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8546), false, "burton", "karen" },
                    { "-26", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8546), false, "eltjo", "burst" },
                    { "-3", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8534), false, "ernst", "karen" },
                    { "-4", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8534), false, "karen", "john" },
                    { "-5", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8535), false, "john", "ernst" },
                    { "-6", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8535), false, "karen", "cena" },
                    { "-7", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8536), false, "burst", "karen" },
                    { "-8", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8536), false, "cena", "ernst" },
                    { "-9", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8537), false, "eltjo", "tijn" },
                    { "0", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8532), false, "eltjo", "tijn" },
                    { "1", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8531), false, "eltjo", "tijn" },
                    { "2", new DateTime(2024, 10, 23, 21, 17, 51, 225, DateTimeKind.Utc).AddTicks(8530), false, "tijn", "eltjo" }
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
