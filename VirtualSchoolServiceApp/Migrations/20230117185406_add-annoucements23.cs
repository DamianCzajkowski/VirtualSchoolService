using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualSchoolServiceApp.Migrations
{
    public partial class addannoucements23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annoucements_Parents_ParentId",
                table: "Annoucements");

            migrationBuilder.DropIndex(
                name: "IX_Annoucements_ParentId",
                table: "Annoucements");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Annoucements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Annoucements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Annoucements_ParentId",
                table: "Annoucements",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annoucements_Parents_ParentId",
                table: "Annoucements",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id");
        }
    }
}
