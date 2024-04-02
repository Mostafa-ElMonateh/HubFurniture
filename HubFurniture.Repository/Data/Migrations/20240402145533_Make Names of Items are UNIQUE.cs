using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeNamesofItemsareUNIQUE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_NameArabic",
                table: "CategoryItems",
                column: "NameArabic",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_NameEnglish",
                table: "CategoryItems",
                column: "NameEnglish",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryItems_NameArabic",
                table: "CategoryItems");

            migrationBuilder.DropIndex(
                name: "IX_CategoryItems_NameEnglish",
                table: "CategoryItems");
        }
    }
}
