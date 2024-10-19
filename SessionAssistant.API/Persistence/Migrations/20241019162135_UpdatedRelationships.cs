using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SessionAssistant.API.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CombatantDTO_Encounters_EncounterDTOId",
                table: "CombatantDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillDTO_CombatantDTO_CombatantDTOId",
                table: "SkillDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillDTO",
                table: "SkillDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CombatantDTO",
                table: "CombatantDTO");

            migrationBuilder.RenameTable(
                name: "SkillDTO",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "CombatantDTO",
                newName: "Combatants");

            migrationBuilder.RenameIndex(
                name: "IX_SkillDTO_CombatantDTOId",
                table: "Skills",
                newName: "IX_Skills_CombatantDTOId");

            migrationBuilder.RenameIndex(
                name: "IX_CombatantDTO_EncounterDTOId",
                table: "Combatants",
                newName: "IX_Combatants_EncounterDTOId");

            migrationBuilder.AlterColumn<int>(
                name: "CombatantDTOId",
                table: "Skills",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EncounterDTOId",
                table: "Combatants",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Combatants",
                table: "Combatants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Combatants_Encounters_EncounterDTOId",
                table: "Combatants",
                column: "EncounterDTOId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Combatants_CombatantDTOId",
                table: "Skills",
                column: "CombatantDTOId",
                principalTable: "Combatants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Combatants_Encounters_EncounterDTOId",
                table: "Combatants");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Combatants_CombatantDTOId",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Combatants",
                table: "Combatants");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "SkillDTO");

            migrationBuilder.RenameTable(
                name: "Combatants",
                newName: "CombatantDTO");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_CombatantDTOId",
                table: "SkillDTO",
                newName: "IX_SkillDTO_CombatantDTOId");

            migrationBuilder.RenameIndex(
                name: "IX_Combatants_EncounterDTOId",
                table: "CombatantDTO",
                newName: "IX_CombatantDTO_EncounterDTOId");

            migrationBuilder.AlterColumn<int>(
                name: "CombatantDTOId",
                table: "SkillDTO",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "EncounterDTOId",
                table: "CombatantDTO",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillDTO",
                table: "SkillDTO",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CombatantDTO",
                table: "CombatantDTO",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CombatantDTO_Encounters_EncounterDTOId",
                table: "CombatantDTO",
                column: "EncounterDTOId",
                principalTable: "Encounters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillDTO_CombatantDTO_CombatantDTOId",
                table: "SkillDTO",
                column: "CombatantDTOId",
                principalTable: "CombatantDTO",
                principalColumn: "Id");
        }
    }
}
