using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoPressure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangedForeignKeyTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Tags_TagId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "Activities",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Tags_TagId",
                table: "Activities",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Tags_TagId",
                table: "Activities");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "Activities",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Tags_TagId",
                table: "Activities",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }
    }
}
