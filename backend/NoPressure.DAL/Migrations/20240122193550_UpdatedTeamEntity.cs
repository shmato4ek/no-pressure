using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoPressure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTeamEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Teams_TeamId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_TeamId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Tags",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TeamId",
                table: "Tags",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Teams_TeamId",
                table: "Tags",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Teams_TeamId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TeamId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Activities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TeamId",
                table: "Activities",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Teams_TeamId",
                table: "Activities",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
