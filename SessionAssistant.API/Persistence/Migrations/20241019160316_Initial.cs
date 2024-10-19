using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SessionAssistant.API.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CanParry = table.Column<bool>(type: "INTEGER", nullable: false),
                    CanDodge = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrentRound = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CombatantDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Initiative = table.Column<int>(type: "INTEGER", nullable: false),
                    Attacks = table.Column<int>(type: "INTEGER", nullable: false),
                    CanAct = table.Column<bool>(type: "INTEGER", nullable: false),
                    EncounterDTOId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatantDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatantDTO_Encounters_EncounterDTOId",
                        column: x => x.EncounterDTOId,
                        principalTable: "Encounters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SkillDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Cooldown = table.Column<int>(type: "INTEGER", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    CombatantDTOId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillDTO_CombatantDTO_CombatantDTOId",
                        column: x => x.CombatantDTOId,
                        principalTable: "CombatantDTO",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "CanDodge", "CanParry", "Name" },
                values: new object[,]
                {
                    { 1, true, true, "Gerlach Bauer" },
                    { 2, false, true, "Roborbor" },
                    { 3, false, false, "Pan Robak" },
                    { 4, true, true, "Chad Poggington" }
                });

            migrationBuilder.InsertData(
                table: "Encounters",
                columns: new[] { "Id", "CurrentRound" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombatantDTO_EncounterDTOId",
                table: "CombatantDTO",
                column: "EncounterDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillDTO_CombatantDTOId",
                table: "SkillDTO",
                column: "CombatantDTOId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "SkillDTO");

            migrationBuilder.DropTable(
                name: "CombatantDTO");

            migrationBuilder.DropTable(
                name: "Encounters");
        }
    }
}
