using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeletePictureCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPictures_CategoryItems_CategoryItemId",
                table: "ProductPictures");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPictures_CategoryItems_CategoryItemId",
                table: "ProductPictures",
                column: "CategoryItemId",
                principalTable: "CategoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPictures_CategoryItems_CategoryItemId",
                table: "ProductPictures");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPictures_CategoryItems_CategoryItemId",
                table: "ProductPictures",
                column: "CategoryItemId",
                principalTable: "CategoryItems",
                principalColumn: "Id");
        }
    }
}
