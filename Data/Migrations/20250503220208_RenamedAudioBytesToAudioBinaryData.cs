using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorProjectApp1.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedAudioBytesToAudioBinaryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AudioBytes",
                table: "RawAudioData",
                newName: "AudioBinaryData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AudioBinaryData",
                table: "RawAudioData",
                newName: "AudioBytes");
        }
    }
}
