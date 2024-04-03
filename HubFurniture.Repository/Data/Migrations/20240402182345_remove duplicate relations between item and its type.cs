using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeduplicaterelationsbetweenitemanditstype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItemsTypes_Categories_CategoryId",
                table: "CategoryItemsTypes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryItemsTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItemsTypes_Categories_CategoryId",
                table: "CategoryItemsTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItemsTypes_Categories_CategoryId",
                table: "CategoryItemsTypes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryItemsTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItemsTypes_Categories_CategoryId",
                table: "CategoryItemsTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
