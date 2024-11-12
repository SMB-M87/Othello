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
                    { "58a479fd-ae6f-4474-a147-68cbdb62c19b", 0, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5730), "[]", "admin" },
                    { "ahmed", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5727), "[]", "Ahmed" },
                    { "briar", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5620), "[]", "Briar" },
                    { "burst", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5618), "[]", "Burst" },
                    { "burton", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5618), "[]", "Burton" },
                    { "cena", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5617), "[]", "Cena" },
                    { "deleted", 0, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5731), "[]", "Deleted" },
                    { "eltjo", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5615), "[]", "Eltjo" },
                    { "ernst", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5613), "[]", "Ernst" },
                    { "ff20c418-f1b0-4f16-b582-294be25c24ef", 0, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5729), "[]", "mediator" },
                    { "gissa", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5624), "[]", "Gissa" },
                    { "hidde", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5626), "[]", "Hidde" },
                    { "identity", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5622), "[]", "Identity" },
                    { "john", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5614), "[]", "John" },
                    { "karen", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5608), "[]", "Karen" },
                    { "lambert", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5621), "[]", "Lambert" },
                    { "nadege", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5728), "[]", "Nadege" },
                    { "nastrovia", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5724), "[]", "Nastrovia" },
                    { "noga", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5626), "[]", "Noga" },
                    { "pedro", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5726), "[]", "Pedro" },
                    { "pipo", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5623), "[]", "Pipo" },
                    { "rachel", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5728), "[]", "Rachel" },
                    { "salie", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5623), "[]", "Salie" },
                    { "tijn", 1, "[]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5616), "[]", "Tijn" }
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
                name: "Results");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
