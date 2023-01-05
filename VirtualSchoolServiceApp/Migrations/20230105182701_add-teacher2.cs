using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualSchoolServiceApp.Migrations
{
    public partial class addteacher2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teacher_SupervisorId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_AspNetUsers_AppUserId",
                table: "Teacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher");

            migrationBuilder.RenameTable(
                name: "Teacher",
                newName: "Teachers");

            migrationBuilder.RenameIndex(
                name: "IX_Teacher_AppUserId",
                table: "Teachers",
                newName: "IX_Teachers_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_SupervisorId",
                table: "Classes",
                column: "SupervisorId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_AspNetUsers_AppUserId",
                table: "Teachers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_SupervisorId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_AspNetUsers_AppUserId",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Teacher");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_AppUserId",
                table: "Teacher",
                newName: "IX_Teacher_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teacher_SupervisorId",
                table: "Classes",
                column: "SupervisorId",
                principalTable: "Teacher",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_AspNetUsers_AppUserId",
                table: "Teacher",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
