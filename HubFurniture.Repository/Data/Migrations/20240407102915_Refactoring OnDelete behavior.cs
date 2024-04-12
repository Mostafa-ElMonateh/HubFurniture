using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringOnDeletebehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItems_Categories_CategoryId",
                table: "CategoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItems_CategoryItemsTypes_CategoryItemTypeId",
                table: "CategoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItemsTypes_Categories_CategoryId",
                table: "CategoryItemsTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySets_Categories_CategoryId",
                table: "CategorySets");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySets_CategorySetsTypes_CategorySetTypeId",
                table: "CategorySets");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySetsTypes_Categories_CategoryId",
                table: "CategorySetsTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerReviews_CategoryItems_CategoryItemId",
                table: "CustomerReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerReviews_CategorySets_CategorySetId",
                table: "CustomerReviews");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategorySetsTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategorySetTypeId",
                table: "CategorySets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategorySets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryItemTypeId",
                table: "CategoryItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItems_Categories_CategoryId",
                table: "CategoryItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItems_CategoryItemsTypes_CategoryItemTypeId",
                table: "CategoryItems",
                column: "CategoryItemTypeId",
                principalTable: "CategoryItemsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItemsTypes_Categories_CategoryId",
                table: "CategoryItemsTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySets_Categories_CategoryId",
                table: "CategorySets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySets_CategorySetsTypes_CategorySetTypeId",
                table: "CategorySets",
                column: "CategorySetTypeId",
                principalTable: "CategorySetsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySetsTypes_Categories_CategoryId",
                table: "CategorySetsTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerReviews_CategoryItems_CategoryItemId",
                table: "CustomerReviews",
                column: "CategoryItemId",
                principalTable: "CategoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerReviews_CategorySets_CategorySetId",
                table: "CustomerReviews",
                column: "CategorySetId",
                principalTable: "CategorySets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItems_Categories_CategoryId",
                table: "CategoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItems_CategoryItemsTypes_CategoryItemTypeId",
                table: "CategoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryItemsTypes_Categories_CategoryId",
                table: "CategoryItemsTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySets_Categories_CategoryId",
                table: "CategorySets");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySets_CategorySetsTypes_CategorySetTypeId",
                table: "CategorySets");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorySetsTypes_Categories_CategoryId",
                table: "CategorySetsTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerReviews_CategoryItems_CategoryItemId",
                table: "CustomerReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerReviews_CategorySets_CategorySetId",
                table: "CustomerReviews");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategorySetsTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategorySetTypeId",
                table: "CategorySets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategorySets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryItemTypeId",
                table: "CategoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItems_Categories_CategoryId",
                table: "CategoryItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItems_CategoryItemsTypes_CategoryItemTypeId",
                table: "CategoryItems",
                column: "CategoryItemTypeId",
                principalTable: "CategoryItemsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryItemsTypes_Categories_CategoryId",
                table: "CategoryItemsTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySets_Categories_CategoryId",
                table: "CategorySets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySets_CategorySetsTypes_CategorySetTypeId",
                table: "CategorySets",
                column: "CategorySetTypeId",
                principalTable: "CategorySetsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySetsTypes_Categories_CategoryId",
                table: "CategorySetsTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerReviews_CategoryItems_CategoryItemId",
                table: "CustomerReviews",
                column: "CategoryItemId",
                principalTable: "CategoryItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerReviews_CategorySets_CategorySetId",
                table: "CustomerReviews",
                column: "CategorySetId",
                principalTable: "CategorySets",
                principalColumn: "Id");
        }
    }
}
