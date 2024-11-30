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
                name: "Logs",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Friends = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requests = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bot = table.Column<int>(type: "int", nullable: false)
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
                    Rematch = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        name: "FK_Games_Players_Rematch",
                        column: x => x.Rematch,
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
                    Forfeit = table.Column<bool>(type: "bit", nullable: false),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                columns: new[] { "Token", "Bot", "Friends", "LastActivity", "Requests", "Username" },
                values: new object[,]
                {
                    { "amy", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(606), "[]", "Amy" },
                    { "andrew", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(600), "[]", "Andrew" },
                    { "Anthony", 2, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(598), "[]", "Anthony" },
                    { "brian", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(604), "[]", "Brian" },
                    { "carol", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(603), "[]", "Carol" },
                    { "deleted", 0, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(607), "[]", "Deleted" },
                    { "donald", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(599), "[]", "Donald" },
                    { "jason", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(605), "[]", "Jason" },
                    { "jeffrey", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(605), "[]", "Jeffrey" },
                    { "jimmy", 2, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(593), "[]", "Jimmy" },
                    { "john", 3, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(592), "[]", "John" },
                    { "kimberly", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(602), "[]", "Kimberly" },
                    { "lisa", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(596), "[]", "Lisa" },
                    { "margaret", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(603), "[]", "Margaret" },
                    { "mary", 2, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(588), "[]", "Mary" },
                    { "matthew", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(598), "[]", "Matthew" },
                    { "michael", 3, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(594), "[]", "Michael" },
                    { "nancy", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(597), "[]", "Nancy" },
                    { "sarah", 3, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(596), "[]", "Sarah" },
                    { "ted", 4, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(594), "[]", "Ted" },
                    { "william", 1, "[]", new DateTime(2024, 11, 30, 0, 27, 7, 326, DateTimeKind.Utc).AddTicks(595), "[]", "William" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_First",
                table: "Games",
                column: "First");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Rematch",
                table: "Games",
                column: "Rematch");

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
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
