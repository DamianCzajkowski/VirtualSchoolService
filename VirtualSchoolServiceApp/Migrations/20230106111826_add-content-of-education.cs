using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualSchoolServiceApp.Migrations
{
    public partial class addcontentofeducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentOfEducation",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentOfEducation",
                table: "Subjects");
        }
    }
}
