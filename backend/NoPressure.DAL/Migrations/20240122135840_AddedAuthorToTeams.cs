using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoPressure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthorToTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AuthorId",
                table: "Teams",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_AuthorId",
                table: "Teams",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_AuthorId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_AuthorId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Teams");
        }
    }
}
