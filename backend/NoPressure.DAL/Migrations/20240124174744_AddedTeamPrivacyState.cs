using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoPressure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeamPrivacyState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrivacyState",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivacyState",
                table: "Teams");
        }
    }
}
