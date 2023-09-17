using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ireview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OnetomanybetweenArticleandTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TagId",
                table: "Articles",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Tags_TagId",
                table: "Articles",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Tags_TagId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_TagId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Articles");
        }
    }
}
