using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddArabicColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductOrdered_ProductName",
                table: "OrderItems",
                newName: "ProductOrdered_ProductNameEnglish");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CategorySetsTypes",
                newName: "NameEnglish");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "CategorySets",
                newName: "StyleEnglish");

            migrationBuilder.RenameColumn(
                name: "Room",
                table: "CategorySets",
                newName: "StyleArabic");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CategorySets",
                newName: "NameEnglish");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CategoryItemsTypes",
                newName: "NameEnglish");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "CategoryItems",
                newName: "StyleEnglish");

            migrationBuilder.RenameColumn(
                name: "Room",
                table: "CategoryItems",
                newName: "StyleArabic");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CategoryItems",
                newName: "NameEnglish");

            migrationBuilder.AddColumn<string>(
                name: "ProductOrdered_ProductNameArabic",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "CategorySetsTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "CategorySets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoomArabic",
                table: "CategorySets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoomEnglish",
                table: "CategorySets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "CategoryItemsTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "CategoryItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoomArabic",
                table: "CategoryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoomEnglish",
                table: "CategoryItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOrdered_ProductNameArabic",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "CategorySetsTypes");

            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "CategorySets");

            migrationBuilder.DropColumn(
                name: "RoomArabic",
                table: "CategorySets");

            migrationBuilder.DropColumn(
                name: "RoomEnglish",
                table: "CategorySets");

            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "CategoryItemsTypes");

            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "CategoryItems");

            migrationBuilder.DropColumn(
                name: "RoomArabic",
                table: "CategoryItems");

            migrationBuilder.DropColumn(
                name: "RoomEnglish",
                table: "CategoryItems");

            migrationBuilder.RenameColumn(
                name: "ProductOrdered_ProductNameEnglish",
                table: "OrderItems",
                newName: "ProductOrdered_ProductName");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                table: "CategorySetsTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StyleEnglish",
                table: "CategorySets",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "StyleArabic",
                table: "CategorySets",
                newName: "Room");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                table: "CategorySets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                table: "CategoryItemsTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StyleEnglish",
                table: "CategoryItems",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "StyleArabic",
                table: "CategoryItems",
                newName: "Room");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                table: "CategoryItems",
                newName: "Name");
        }
    }
}
