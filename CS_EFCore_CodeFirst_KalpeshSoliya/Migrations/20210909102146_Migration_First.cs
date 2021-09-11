using Microsoft.EntityFrameworkCore.Migrations;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Migrations
{
    public partial class Migration_First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryRowId);
                });

            migrationBuilder.CreateTable(
                name: "Venders",
                columns: table => new
                {
                    VenderRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenderId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VenderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venders", x => x.VenderRowId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryRowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductRowId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryRowId",
                        column: x => x.CategoryRowId,
                        principalTable: "Categories",
                        principalColumn: "CategoryRowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorProduct",
                columns: table => new
                {
                    VenderProductRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenderRowId = table.Column<int>(type: "int", nullable: false),
                    ProductRowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorProduct", x => x.VenderProductRowId);
                    table.ForeignKey(
                        name: "FK_VendorProduct_Products_ProductRowId",
                        column: x => x.ProductRowId,
                        principalTable: "Products",
                        principalColumn: "ProductRowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorProduct_Venders_VenderRowId",
                        column: x => x.VenderRowId,
                        principalTable: "Venders",
                        principalColumn: "VenderRowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryRowId",
                table: "Products",
                column: "CategoryRowId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProduct_ProductRowId",
                table: "VendorProduct",
                column: "ProductRowId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProduct_VenderRowId",
                table: "VendorProduct",
                column: "VenderRowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorProduct");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Venders");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
