using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualSchoolServiceApp.Migrations
{
    public partial class eee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messageThreads_AspNetUsers_ApplicationUserId",
                table: "messageThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_messageThreads_Messages_MessageId",
                table: "messageThreads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messageThreads",
                table: "messageThreads");

            migrationBuilder.RenameTable(
                name: "messageThreads",
                newName: "MessageThreads");

            migrationBuilder.RenameIndex(
                name: "IX_messageThreads_MessageId",
                table: "MessageThreads",
                newName: "IX_MessageThreads_MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_messageThreads_ApplicationUserId",
                table: "MessageThreads",
                newName: "IX_MessageThreads_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageThreads",
                table: "MessageThreads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreads_AspNetUsers_ApplicationUserId",
                table: "MessageThreads",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreads_Messages_MessageId",
                table: "MessageThreads",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreads_AspNetUsers_ApplicationUserId",
                table: "MessageThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreads_Messages_MessageId",
                table: "MessageThreads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageThreads",
                table: "MessageThreads");

            migrationBuilder.RenameTable(
                name: "MessageThreads",
                newName: "messageThreads");

            migrationBuilder.RenameIndex(
                name: "IX_MessageThreads_MessageId",
                table: "messageThreads",
                newName: "IX_messageThreads_MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageThreads_ApplicationUserId",
                table: "messageThreads",
                newName: "IX_messageThreads_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messageThreads",
                table: "messageThreads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_messageThreads_AspNetUsers_ApplicationUserId",
                table: "messageThreads",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_messageThreads_Messages_MessageId",
                table: "messageThreads",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }
    }
}
