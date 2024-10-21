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
                    { "burst", "[\"Cena\",\"Karen\"]", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8679), "[]", "Burst" },
                    { "burton", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8681), "[]", "Burton" },
                    { "cena", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8678), "[]", "Cena" },
                    { "eltjo", "[\"Tijn\"]", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8666), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-21T04:24:57.6428667Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-21T04:24:57.6428672Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-21T04:24:57.6428673Z\"}]", "Eltjo" },
                    { "ernst", "[\"John\",\"Karen\"]", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8662), "[]", "Ernst" },
                    { "john", "[\"Ernst\",\"Karen\"]", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8664), "[]", "John" },
                    { "karen", "[\"Ernst\",\"John\"]", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8657), "[]", "Karen" },
                    { "tijn", "[\"Eltjo\"]", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8674), "[{\"Type\":0,\"Username\":\"Karen\",\"Date\":\"2024-10-21T04:24:57.6428675Z\"},{\"Type\":0,\"Username\":\"Ernst\",\"Date\":\"2024-10-21T04:24:57.6428676Z\"},{\"Type\":0,\"Username\":\"John\",\"Date\":\"2024-10-21T04:24:57.6428677Z\"}]", "Tijn" }
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
                    { "-0", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8949), false, "eltjo", "tijn" },
                    { "-1", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8949), false, "eltjo", "tijn" },
                    { "-10", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8953), false, "john", "eltjo" },
                    { "-11", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8954), false, "ernst", "john" },
                    { "-12", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8954), false, "john", "cena" },
                    { "-13", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8955), false, "tijn", "burst" },
                    { "-14", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8955), false, "karen", "tijn" },
                    { "-15", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8956), false, "ernst", "burton" },
                    { "-16", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8956), false, "john", "burton" },
                    { "-17", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8957), false, "ernst", "cena" },
                    { "-18", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8957), false, "karen", "eltjo" },
                    { "-19", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8957), false, "cena", "burst" },
                    { "-2", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8948), false, "tijn", "eltjo" },
                    { "-20", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8958), false, "burton", "ernst" },
                    { "-21", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8959), false, "john", "tijn" },
                    { "-22", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8959), false, "eltjo", "karen" },
                    { "-23", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8960), false, "burst", "john" },
                    { "-24", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8960), false, "tijn", "cena" },
                    { "-25", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8961), false, "burton", "karen" },
                    { "-26", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8961), false, "eltjo", "burst" },
                    { "-3", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8950), false, "ernst", "karen" },
                    { "-4", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8950), false, "karen", "john" },
                    { "-5", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8951), false, "john", "ernst" },
                    { "-6", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8951), false, "karen", "cena" },
                    { "-7", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8952), false, "burst", "karen" },
                    { "-8", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8952), false, "cena", "ernst" },
                    { "-9", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8953), false, "eltjo", "tijn" },
                    { "0", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8948), false, "eltjo", "tijn" },
                    { "1", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8947), false, "eltjo", "tijn" },
                    { "2", new DateTime(2024, 10, 21, 4, 24, 57, 642, DateTimeKind.Utc).AddTicks(8946), false, "tijn", "eltjo" }
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

            migrationBuilder.Sql(@"
           CREATE TRIGGER PreventResultChanges
           ON Results
           INSTEAD OF UPDATE
           AS
           BEGIN
               IF UPDATE(Token) OR UPDATE(Winner) OR UPDATE(Loser) OR UPDATE(Draw)
               BEGIN
                   IF UPDATE(Token)
                   BEGIN
                       RAISERROR('Cannot update the Token. This field is protected.', 16, 1);
                       ROLLBACK TRANSACTION;
                   END

                   IF UPDATE(Winner)
                   BEGIN
                       RAISERROR('Cannot update the Winner. This field is protected by a foreign key constraint.', 16, 1);
                       ROLLBACK TRANSACTION;
                   END

                   IF UPDATE(Loser)
                   BEGIN
                       RAISERROR('Cannot update the Loser. This field is protected by a foreign key constraint.', 16, 1);
                       ROLLBACK TRANSACTION;
                   END

                   IF UPDATE(Draw)
                   BEGIN
                       RAISERROR('Cannot update the Draw. This field is protected.', 16, 1);
                       ROLLBACK TRANSACTION;
                   END
               END
               ELSE
               BEGIN
                   -- Allow other updates if neither Winner nor Loser is being modified
                   UPDATE Results
                   SET Date = inserted.Date
                   FROM inserted
                   WHERE Results.Token = inserted.Token;
               END
           END;
       ");

            migrationBuilder.Sql(@"
           CREATE TRIGGER PreventPlayerChanges
           ON Players
           INSTEAD OF UPDATE
           AS
           BEGIN
               IF UPDATE(Token) OR UPDATE(Username)
               BEGIN
                   IF UPDATE(Token)
                   BEGIN
                       RAISERROR('Cannot update the Token. This field is protected.', 16, 1);
                       ROLLBACK TRANSACTION;
                   END

                   IF UPDATE(Username)
                   BEGIN
                       RAISERROR('Cannot update the Draw. This field is protected.', 16, 1);
                       ROLLBACK TRANSACTION;
                   END
               END
               ELSE
               BEGIN
                   -- Allow other updates if Username is not being modified
                   UPDATE Players
                   SET LastActivity = inserted.LastActivity,
                       Friends = inserted.Friends,
                       Requests = inserted.Requests
                   FROM inserted
                   WHERE Players.Token = inserted.Token;
               END
           END;
       ");

            migrationBuilder.Sql(@"
           CREATE TRIGGER PreventGameChanges
           ON Games
           INSTEAD OF UPDATE
           AS
           BEGIN
               -- Prevent changes to Token
               IF UPDATE(Token)
               BEGIN
                   RAISERROR('Cannot update Token. This field is immutable.', 16, 1);
                   ROLLBACK TRANSACTION;
                   RETURN;
               END
               
               -- Prevent changes to First
               IF UPDATE(First)
               BEGIN
                   RAISERROR('Cannot update First. This field is immutable.', 16, 1);
                   ROLLBACK TRANSACTION;
                   RETURN;
               END
           
               -- Prevent updates to Second if it is already set (i.e. not null)
               IF UPDATE(Second)
               BEGIN
                   DECLARE @newSecond nvarchar(450), @oldSecond nvarchar(450);
                   SELECT @newSecond = inserted.Second, @oldSecond = deleted.Second
                   FROM inserted INNER JOIN deleted ON inserted.Token = deleted.Token;
           
                   IF @oldSecond IS NOT NULL AND @newSecond <> @oldSecond
                   BEGIN
                       RAISERROR('Cannot update Second once it is set.', 16, 1);
                       ROLLBACK TRANSACTION;
                       RETURN;
                   END
               END
           
               -- Proceed with the update if the rules are respected
               UPDATE Games
               SET Description = inserted.Description,
                   Status = inserted.Status,
                   PlayersTurn = inserted.PlayersTurn,
                   FColor = inserted.FColor,
                   SColor = inserted.SColor,
                   Second = inserted.Second -- this only allows the first setting of Second
               FROM inserted
               WHERE Games.Token = inserted.Token;
           END;
       ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS PreventResultChanges");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS PreventPlayerChanges");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS PreventGameChanges");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
