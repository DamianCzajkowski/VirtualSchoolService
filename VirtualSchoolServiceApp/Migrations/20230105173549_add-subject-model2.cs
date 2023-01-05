using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualSchoolServiceApp.Migrations
{
    public partial class addsubjectmodel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjects_Classes_ClassesId",
                table: "ClassSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjects_Subjects_SubjectsId",
                table: "ClassSubjects");

            migrationBuilder.RenameColumn(
                name: "SubjectsId",
                table: "ClassSubjects",
                newName: "ClassId");

            migrationBuilder.RenameColumn(
                name: "ClassesId",
                table: "ClassSubjects",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSubjects_SubjectsId",
                table: "ClassSubjects",
                newName: "IX_ClassSubjects_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjects_Classes_ClassId",
                table: "ClassSubjects",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjects_Subjects_SubjectId",
                table: "ClassSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjects_Classes_ClassId",
                table: "ClassSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSubjects_Subjects_SubjectId",
                table: "ClassSubjects");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "ClassSubjects",
                newName: "SubjectsId");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "ClassSubjects",
                newName: "ClassesId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassSubjects_ClassId",
                table: "ClassSubjects",
                newName: "IX_ClassSubjects_SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjects_Classes_ClassesId",
                table: "ClassSubjects",
                column: "ClassesId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSubjects_Subjects_SubjectsId",
                table: "ClassSubjects",
                column: "SubjectsId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
