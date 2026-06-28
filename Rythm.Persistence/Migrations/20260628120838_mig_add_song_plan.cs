using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rythm.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_song_plan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequiredPlan",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredPlan",
                table: "Songs");
        }
    }
}
