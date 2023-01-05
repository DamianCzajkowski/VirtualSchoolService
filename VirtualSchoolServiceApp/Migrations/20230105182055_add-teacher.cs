using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualSchoolServiceApp.Migrations
{
    public partial class addteacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTeacher",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teacher_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SupervisorId",
                table: "Classes",
                column: "SupervisorId",
                unique: true,
                filter: "[SupervisorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_AppUserId",
                table: "Teacher",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teacher_SupervisorId",
                table: "Classes",
                column: "SupervisorId",
                principalTable: "Teacher",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teacher_SupervisorId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SupervisorId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "IsTeacher",
                table: "AspNetUsers");
        }
    }
}
