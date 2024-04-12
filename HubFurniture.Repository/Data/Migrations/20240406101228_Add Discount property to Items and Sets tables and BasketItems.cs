using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountpropertytoItemsandSetstablesandBasketItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "CategorySets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "CategoryItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CategorySets");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CategoryItems");
        }
    }
}
