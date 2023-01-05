using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualSchoolServiceApp.Migrations
{
    public partial class addisstudentfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStudent",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStudent",
                table: "AspNetUsers");
        }
    }
}
