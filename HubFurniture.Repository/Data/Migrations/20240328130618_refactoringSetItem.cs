using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class refactoringSetItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SetItems",
                newName: "NameEnglish");

            migrationBuilder.AddColumn<string>(
                name: "NameArabic",
                table: "SetItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameArabic",
                table: "SetItems");

            migrationBuilder.RenameColumn(
                name: "NameEnglish",
                table: "SetItems",
                newName: "Name");
        }
    }
}
