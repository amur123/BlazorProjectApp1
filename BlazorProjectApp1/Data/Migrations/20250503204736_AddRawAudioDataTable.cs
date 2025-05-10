using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorProjectApp1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRawAudioDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawAudioData",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    AudioId = table.Column<int>(type: "INTEGER", nullable: false),
                    AudioBytes = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawAudioData", x => new { x.PostId, x.AudioId });
                    table.ForeignKey(
                        name: "FK_RawAudioData_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RawAudioData_PostId",
                table: "RawAudioData",
                column: "PostId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawAudioData");
        }
    }
}
