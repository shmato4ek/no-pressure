using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoPressure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_FollowerId1",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_FollowingId1",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_FollowerId1",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_FollowingId1",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "FollowerId1",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "FollowingId1",
                table: "Subscriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FollowerId1",
                table: "Subscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FollowingId1",
                table: "Subscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_FollowerId1",
                table: "Subscriptions",
                column: "FollowerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_FollowingId1",
                table: "Subscriptions",
                column: "FollowingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_FollowerId1",
                table: "Subscriptions",
                column: "FollowerId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_FollowingId1",
                table: "Subscriptions",
                column: "FollowingId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
