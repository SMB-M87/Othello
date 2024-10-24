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
                    { "badbux", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8838), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T23:42:14.1338839Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T23:42:14.133884Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T23:42:14.133884Z\"}]", "Badbux" },
                    { "burst", "[\"Cena\",\"Karen\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8785), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T23:42:14.1338787Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T23:42:14.1338788Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T23:42:14.1338788Z\"}]", "Burst" },
                    { "burton", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8789), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T23:42:14.133879Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T23:42:14.1338791Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T23:42:14.1338832Z\"}]", "Burton" },
                    { "cena", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8782), "[{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T23:42:14.1338783Z\"},{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T23:42:14.1338784Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T23:42:14.1338785Z\"}]", "Cena" },
                    { "eltjo", "[\"Tijn\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8775), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-23T23:42:14.1338776Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-23T23:42:14.1338777Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-23T23:42:14.1338778Z\"}]", "Eltjo" },
                    { "ernst", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8768), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T23:42:14.1338769Z\"},{\"Type\":0,\"Username\":\"Burst\",\"Date\":\"2024-10-23T23:42:14.133877Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T23:42:14.1338771Z\"}]", "Ernst" },
                    { "identity", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8841), "[{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T23:42:14.1338842Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T23:42:14.1338843Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T23:42:14.1338844Z\"}]", "Identity" },
                    { "john", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8772), "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T23:42:14.1338773Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T23:42:14.1338774Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T23:42:14.1338774Z\"}]", "John" },
                    { "karen", "[\"Ernst\",\"John\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8759), "[{\"Type\":0,\"Username\":\"Tijn\",\"Date\":\"2024-10-23T23:42:14.1338764Z\"},{\"Type\":0,\"Username\":\"Eltjo\",\"Date\":\"2024-10-23T23:42:14.1338766Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T23:42:14.1338766Z\"}]", "Karen" },
                    { "sadbux", "[\"Eltjo\",\"Tijn\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8834), "[{\"Type\":0,\"Username\":\"Cena\",\"Date\":\"2024-10-23T23:42:14.1338835Z\"},{\"Type\":0,\"Username\":\"Burton\",\"Date\":\"2024-10-23T23:42:14.1338836Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-23T23:42:14.1338837Z\"}]", "Sadbux" },
                    { "tijn", "[\"Eltjo\"]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8779), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-23T23:42:14.133878Z\"},{\"Type\":0,\"Username\":\"Badbux\",\"Date\":\"2024-10-23T23:42:14.1338781Z\"},{\"Type\":0,\"Username\":\"Sadbux\",\"Date\":\"2024-10-23T23:42:14.1338781Z\"}]", "Tijn" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Token", "Board", "Date", "Description", "FColor", "First", "PlayersTurn", "SColor", "Second", "Status" },
                values: new object[,]
                {
                    { "four", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8994), "I wanna play a game and don't have any requirements!", 2, "burst", 2, 1, null, 0 },
                    { "one", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8982), "I search an advanced player!", 2, "ernst", 2, 1, "john", 1 },
                    { "three", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8990), "I wanna play a game and don't have any requirements!", 2, "cena", 2, 1, null, 0 },
                    { "two", "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8985), "I want to player more than one game against the same adversary.", 2, "eltjo", 1, 1, "tijn", 1 },
                    { "zero", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(8977), "I wanna play a game and don't have any requirements!", 2, "karen", 2, 1, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Token", "Date", "Draw", "Loser", "Winner" },
                values: new object[,]
                {
                    { "-0", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9014), false, "eltjo", "tijn" },
                    { "-1", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9013), false, "eltjo", "tijn" },
                    { "-10", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9018), false, "john", "eltjo" },
                    { "-11", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9018), false, "ernst", "john" },
                    { "-12", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9019), false, "john", "cena" },
                    { "-13", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9019), false, "tijn", "burst" },
                    { "-14", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9020), false, "karen", "tijn" },
                    { "-15", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9020), false, "ernst", "burton" },
                    { "-16", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9021), false, "john", "burton" },
                    { "-17", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9021), false, "ernst", "cena" },
                    { "-18", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9022), false, "karen", "eltjo" },
                    { "-19", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9022), false, "cena", "burst" },
                    { "-2", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9012), false, "tijn", "eltjo" },
                    { "-20", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9023), false, "burton", "ernst" },
                    { "-21", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9023), false, "john", "tijn" },
                    { "-22", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9024), false, "eltjo", "karen" },
                    { "-23", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9025), false, "burst", "john" },
                    { "-24", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9026), false, "tijn", "cena" },
                    { "-25", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9028), false, "burton", "karen" },
                    { "-26", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9028), false, "eltjo", "burst" },
                    { "-3", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9014), false, "ernst", "karen" },
                    { "-4", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9015), false, "karen", "john" },
                    { "-5", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9015), false, "john", "ernst" },
                    { "-6", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9016), false, "karen", "cena" },
                    { "-7", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9016), false, "burst", "karen" },
                    { "-8", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9017), false, "cena", "ernst" },
                    { "-9", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9017), false, "eltjo", "tijn" },
                    { "0", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9012), false, "eltjo", "tijn" },
                    { "1", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9011), false, "eltjo", "tijn" },
                    { "2", new DateTime(2024, 10, 23, 23, 42, 14, 133, DateTimeKind.Utc).AddTicks(9010), false, "tijn", "eltjo" }
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
