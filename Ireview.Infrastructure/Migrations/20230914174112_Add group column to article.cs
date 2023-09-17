using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ireview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addgroupcolumntoarticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Articles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Articles");
        }
    }
}
