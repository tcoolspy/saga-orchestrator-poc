using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AwesomeWebServiceOne.Migrations
{
    /// <inheritdoc />
    public partial class addingbloguser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogUserId",
                table: "Blogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogUserId",
                table: "Blogs");
        }
    }
}
