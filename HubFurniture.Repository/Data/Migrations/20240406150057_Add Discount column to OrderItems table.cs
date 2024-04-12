using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountcolumntoOrderItemstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "OrderItems");
        }
    }
}
