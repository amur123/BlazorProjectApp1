using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorProjectApp1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOcrImageTextToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OcrImageText",
                table: "Posts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "OcrImageText",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "OcrImageText",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3,
                column: "OcrImageText",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 4,
                column: "OcrImageText",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 5,
                column: "OcrImageText",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 6,
                column: "OcrImageText",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OcrImageText",
                table: "Posts");
        }
    }
}
