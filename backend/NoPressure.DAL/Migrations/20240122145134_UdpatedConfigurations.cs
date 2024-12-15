using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NoPressure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UdpatedConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationData_DataId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationData");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_DataId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "DataId",
                table: "Notifications",
                newName: "Data_Id");

            migrationBuilder.AddColumn<string>(
                name: "Data_GoalName",
                table: "Notifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data_Link",
                table: "Notifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data_SecondUserName",
                table: "Notifications",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data_GoalName",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Data_Link",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Data_SecondUserName",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Data_Id",
                table: "Notifications",
                newName: "DataId");

            migrationBuilder.CreateTable(
                name: "NotificationData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoalName = table.Column<string>(type: "text", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: true),
                    SecondUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DataId",
                table: "Notifications",
                column: "DataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationData_DataId",
                table: "Notifications",
                column: "DataId",
                principalTable: "NotificationData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
