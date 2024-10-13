using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PlayersTurn = table.Column<int>(type: "int", nullable: false),
                    First = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FColor = table.Column<int>(type: "int", nullable: false),
                    Second = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SColor = table.Column<int>(type: "int", nullable: false),
                    Board = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Friends = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PendingFriends = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Winner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Loser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Draw = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Token);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Token", "Board", "Description", "FColor", "First", "PlayersTurn", "SColor", "Second", "Status" },
                values: new object[,]
                {
                    { "one", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", "I search an advanced player!", 2, "ernst", 2, 1, "john", 1 },
                    { "two", "[[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,2,2],[1,1,1,1,1,1,2,2],[1,1,1,1,1,1,1,2],[1,1,1,1,1,1,1,1]]", "I want to player more than one game against the same adversary.", 2, " eltjo", 0, 1, "tijn", 2 },
                    { "zero", "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]", "I wanna play a game and don't have any requirements.", 2, "karen", 2, 1, "", 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Token", "Friends", "PendingFriends", "Username" },
                values: new object[,]
                {
                    { " eltjo", "[]", "[]", "Eltjo" },
                    { "ernst", "[]", "[]", "Ersnt" },
                    { "john", "[]", "[]", "John" },
                    { "karen", "[]", "[]", "Karen" },
                    { "tijn", "[]", "[]", "Tijn" }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Token", "Draw", "Loser", "Winner" },
                values: new object[,]
                {
                    { "-1", "Empty", " eltjo", "tijn" },
                    { "-2", "Empty", "tijn", " eltjo" },
                    { "two", "Empty", " eltjo", "tijn" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
