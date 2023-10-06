using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoPressure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangePlanState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGoal",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Plans",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Plans");

            migrationBuilder.AddColumn<bool>(
                name: "IsGoal",
                table: "Plans",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
