using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeColumntoOrderedItemtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductOrdered_Type",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOrdered_Type",
                table: "OrderItems");
        }
    }
}
