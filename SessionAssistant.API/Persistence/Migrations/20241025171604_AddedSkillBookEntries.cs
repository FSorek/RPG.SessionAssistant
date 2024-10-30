using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SessionAssistant.API.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedSkillBookEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "SkillBookEntryId",
                table: "Skills",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SkillBookEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillBookEntries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SkillBookEntries",
                columns: new[] { "Id", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "Just dodge", "", "Unik" },
                    { 2, "PARRY THIS", "", "Parowanie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillBookEntryId",
                table: "Skills",
                column: "SkillBookEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_SkillBookEntries_SkillBookEntryId",
                table: "Skills",
                column: "SkillBookEntryId",
                principalTable: "SkillBookEntries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_SkillBookEntries_SkillBookEntryId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "SkillBookEntries");

            migrationBuilder.DropIndex(
                name: "IX_Skills_SkillBookEntryId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "SkillBookEntryId",
                table: "Skills");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Skills",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Skills",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Skills",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
