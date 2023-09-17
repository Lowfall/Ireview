using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ireview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Editnamingusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Users_UserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Users_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UsersProfiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersProfiles",
                table: "UsersProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_UsersProfiles_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "UsersProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UsersProfiles_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "UsersProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_UsersProfiles_UserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UsersProfiles_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersProfiles",
                table: "UsersProfiles");

            migrationBuilder.RenameTable(
                name: "UsersProfiles",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Users_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Users_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
