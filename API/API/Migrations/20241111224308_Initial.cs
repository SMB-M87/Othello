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

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Token", "Board", "Date", "Draw", "Forfeit", "Loser", "Winner" },
                values: new object[,]
                {
                    { "-0", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5963), false, false, "eltjo", "tijn" },
                    { "-1", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5962), false, false, "eltjo", "tijn" },
                    { "-10", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5969), false, false, "john", "eltjo" },
                    { "-11", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5969), false, false, "ernst", "john" },
                    { "-12", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5970), false, false, "john", "cena" },
                    { "-13", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5971), false, false, "tijn", "burst" },
                    { "-14", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5971), false, false, "karen", "tijn" },
                    { "-15", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5972), false, false, "ernst", "burton" },
                    { "-16", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5972), false, false, "john", "burton" },
                    { "-17", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5973), false, false, "ernst", "cena" },
                    { "-18", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5973), false, false, "karen", "eltjo" },
                    { "-19", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5974), false, false, "cena", "burst" },
                    { "-2", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5961), false, false, "tijn", "eltjo" },
                    { "-20", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5975), false, false, "burton", "ernst" },
                    { "-21", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5975), false, false, "john", "tijn" },
                    { "-22", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5976), false, false, "eltjo", "karen" },
                    { "-23", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5977), false, false, "burst", "john" },
                    { "-24", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5977), false, false, "tijn", "cena" },
                    { "-25", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5978), false, false, "burton", "karen" },
                    { "-26", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5978), false, false, "eltjo", "burst" },
                    { "-27", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5979), true, false, "pipo", "salie" },
                    { "-28", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5980), true, false, "gissa", "pipo" },
                    { "-29", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5980), false, false, "hidde", "gissa" },
                    { "-3", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5964), false, false, "ernst", "karen" },
                    { "-30", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5981), false, false, "noga", "hidde" },
                    { "-31", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5982), true, false, "nastrovia", "noga" },
                    { "-32", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5982), true, false, "pedro", "nastrovia" },
                    { "-33", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5983), false, false, "ahmed", "pedro" },
                    { "-34", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5984), false, false, "nadege", "ahmed" },
                    { "-35", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5985), true, false, "rachel", "nadege" },
                    { "-36", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5989), true, false, "salie", "rachel" },
                    { "-37", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5990), false, false, "hidde", "pipo" },
                    { "-38", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5991), false, false, "pedro", "noga" },
                    { "-39", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5992), false, false, "ahmed", "nastrovia" },
                    { "-4", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5965), false, false, "karen", "john" },
                    { "-40", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5993), false, false, "rachel", "pedro" },
                    { "-41", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5994), true, false, "pipo", "ahmed" },
                    { "-42", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5995), false, false, "nastrovia", "gissa" },
                    { "-43", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5997), false, false, "ahmed", "hidde" },
                    { "-44", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5997), false, false, "noga", "rachel" },
                    { "-45", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5998), false, false, "salie", "karen" },
                    { "-46", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5999), true, false, "pipo", "ernst" },
                    { "-47", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6000), false, false, "gissa", "john" },
                    { "-48", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6000), true, false, "hidde", "eltjo" },
                    { "-49", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6001), false, false, "noga", "tijn" },
                    { "-5", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5966), false, false, "john", "ernst" },
                    { "-50", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6001), false, false, "nastrovia", "cena" },
                    { "-51", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6002), true, false, "pedro", "burst" },
                    { "-52", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6003), false, false, "ahmed", "burton" },
                    { "-53", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6003), false, false, "nadege", "briar" },
                    { "-54", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6004), true, false, "rachel", "lambert" },
                    { "-55", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6005), false, false, "salie", "identity" },
                    { "-56", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6006), false, false, "ernst", "pipo" },
                    { "-57", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6006), true, false, "john", "gissa" },
                    { "-58", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6008), false, false, "tijn", "hidde" },
                    { "-59", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6008), false, false, "burst", "noga" },
                    { "-6", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5966), false, false, "karen", "cena" },
                    { "-60", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6009), false, false, "burton", "nastrovia" },
                    { "-61", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6009), true, false, "briar", "pedro" },
                    { "-62", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6010), false, false, "identity", "ahmed" },
                    { "-63", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6010), true, false, "lambert", "nadege" },
                    { "-64", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6011), false, false, "john", "rachel" },
                    { "-65", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6012), true, false, "cena", "karen" },
                    { "-66", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6012), false, false, "tijn", "burst" },
                    { "-67", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6013), false, false, "burton", "ernst" },
                    { "-68", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6013), true, false, "lambert", "john" },
                    { "-69", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6014), false, false, "gissa", "identity" },
                    { "-7", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5967), false, false, "burst", "karen" },
                    { "-70", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6015), true, false, "ernst", "salie" },
                    { "-71", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6015), false, false, "john", "pipo" },
                    { "-72", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6016), false, false, "eltjo", "hidde" },
                    { "-73", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6016), true, false, "burst", "nastrovia" },
                    { "-74", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6017), false, false, "briar", "nadege" },
                    { "-75", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6017), false, false, "tijn", "noga" },
                    { "-76", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6018), false, false, "lambert", "pedro" },
                    { "-77", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6019), true, false, "cena", "ahmed" },
                    { "-78", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6021), false, false, "burton", "rachel" },
                    { "-79", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6022), true, false, "noga", "tijn" },
                    { "-8", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5968), false, false, "cena", "ernst" },
                    { "-80", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(6022), false, false, "pedro", "burton" },
                    { "-9", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5968), false, false, "eltjo", "tijn" },
                    { "0", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5961), false, false, "eltjo", "tijn" },
                    { "1", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5960), false, false, "eltjo", "tijn" },
                    { "2", "[[2,2,2,2,2,2,2,2],[1,1,1,1,2,2,2,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,2,1,2,2],[1,1,2,1,2,1,2,2],[1,2,2,2,1,2,1,2],[2,2,2,2,2,2,2,2]]", new DateTime(2024, 11, 11, 22, 43, 8, 644, DateTimeKind.Utc).AddTicks(5958), false, false, "tijn", "eltjo" }
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
