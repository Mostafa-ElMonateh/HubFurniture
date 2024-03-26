using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class delivaryMethodLocalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "DeliveryMethods",
                newName: "DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "DeliveryMethods",
                newName: "DescriptionArabic");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTimeArabic",
                table: "DeliveryMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTimeEnglish",
                table: "DeliveryMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryTimeArabic",
                table: "DeliveryMethods");

            migrationBuilder.DropColumn(
                name: "DeliveryTimeEnglish",
                table: "DeliveryMethods");

            migrationBuilder.RenameColumn(
                name: "DescriptionEnglish",
                table: "DeliveryMethods",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "DescriptionArabic",
                table: "DeliveryMethods",
                newName: "DeliveryTime");
        }
    }
}
