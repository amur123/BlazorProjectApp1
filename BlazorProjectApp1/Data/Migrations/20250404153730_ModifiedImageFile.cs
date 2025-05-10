using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorProjectApp1.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedImageFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFile",
                table: "Posts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "ImageFile",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "ImageFile",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3,
                column: "ImageFile",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 4,
                column: "ImageFile",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 5,
                column: "ImageFile",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 6,
                column: "ImageFile",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Posts");
        }
    }
}
