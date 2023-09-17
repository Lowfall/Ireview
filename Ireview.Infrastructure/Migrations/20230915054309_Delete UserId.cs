using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ireview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UsersProfiles_UserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Href",
                table: "Tags",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UsersProfiles_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "UsersProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UsersProfiles_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Href",
                table: "Tags");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "UserId",
                keyValue: null,
                column: "UserId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UsersProfiles_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "UsersProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
