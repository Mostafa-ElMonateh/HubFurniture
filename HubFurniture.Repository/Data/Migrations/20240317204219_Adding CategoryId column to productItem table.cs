using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingCategoryIdcolumntoproductItemtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProductItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_CategoryId",
                table: "ProductItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_Categories_CategoryId",
                table: "ProductItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_Categories_CategoryId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_CategoryId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductItems");
        }
    }
}
