using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SessionAssistant.API.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCombatantLastAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastAction",
                table: "Combatants",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAction",
                table: "Combatants");
        }
    }
}
