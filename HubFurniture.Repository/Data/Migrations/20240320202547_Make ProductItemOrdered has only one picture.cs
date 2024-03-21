using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeProductItemOrderedhasonlyonepicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductOrdered_PictureUrls",
                table: "OrderItems",
                newName: "ProductOrdered_PictureUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductOrdered_PictureUrl",
                table: "OrderItems",
                newName: "ProductOrdered_PictureUrls");
        }
    }
}
