using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubFurniture.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryItemsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItemsTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryItemsTypes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorySetsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySetsTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySetsTypes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Availability = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suitability = table.Column<byte>(type: "tinyint", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Depth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CategoryItemTypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryItems_CategoryItemsTypes_CategoryItemTypeId",
                        column: x => x.CategoryItemTypeId,
                        principalTable: "CategoryItemsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategorySets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Availability = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suitability = table.Column<byte>(type: "tinyint", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategorySetTypeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategorySets_CategorySetsTypes_CategorySetTypeId",
                        column: x => x.CategorySetTypeId,
                        principalTable: "CategorySetsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryItemCategorySet",
                columns: table => new
                {
                    CategoryItemsId = table.Column<int>(type: "int", nullable: false),
                    CategorySetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItemCategorySet", x => new { x.CategoryItemsId, x.CategorySetsId });
                    table.ForeignKey(
                        name: "FK_CategoryItemCategorySet_CategoryItems_CategoryItemsId",
                        column: x => x.CategoryItemsId,
                        principalTable: "CategoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryItemCategorySet_CategorySets_CategorySetsId",
                        column: x => x.CategorySetsId,
                        principalTable: "CategorySets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategorySetId = table.Column<int>(type: "int", nullable: true),
                    CategoryItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerReviews_CategoryItems_CategoryItemId",
                        column: x => x.CategoryItemId,
                        principalTable: "CategoryItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerReviews_CategorySets_CategorySetId",
                        column: x => x.CategorySetId,
                        principalTable: "CategorySets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductPictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategorySetId = table.Column<int>(type: "int", nullable: true),
                    CategoryItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPictures_CategoryItems_CategoryItemId",
                        column: x => x.CategoryItemId,
                        principalTable: "CategoryItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductPictures_CategorySets_CategorySetId",
                        column: x => x.CategorySetId,
                        principalTable: "CategorySets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItemCategorySet_CategorySetsId",
                table: "CategoryItemCategorySet",
                column: "CategorySetsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_CategoryId",
                table: "CategoryItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_CategoryItemTypeId",
                table: "CategoryItems",
                column: "CategoryItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItemsTypes_CategoryId",
                table: "CategoryItemsTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySets_CategoryId",
                table: "CategorySets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySets_CategorySetTypeId",
                table: "CategorySets",
                column: "CategorySetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySetsTypes_CategoryId",
                table: "CategorySetsTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReviews_CategoryItemId",
                table: "CustomerReviews",
                column: "CategoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReviews_CategorySetId",
                table: "CustomerReviews",
                column: "CategorySetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictures_CategoryItemId",
                table: "ProductPictures",
                column: "CategoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPictures_CategorySetId",
                table: "ProductPictures",
                column: "CategorySetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryItemCategorySet");

            migrationBuilder.DropTable(
                name: "CustomerReviews");

            migrationBuilder.DropTable(
                name: "ProductPictures");

            migrationBuilder.DropTable(
                name: "CategoryItems");

            migrationBuilder.DropTable(
                name: "CategorySets");

            migrationBuilder.DropTable(
                name: "CategoryItemsTypes");

            migrationBuilder.DropTable(
                name: "CategorySetsTypes");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
