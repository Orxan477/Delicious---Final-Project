using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class ChangeTableNameProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prouducts_Categories_CategoryId",
                table: "Prouducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Prouducts_MenuImages_MenuImageId",
                table: "Prouducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prouducts",
                table: "Prouducts");

            migrationBuilder.RenameTable(
                name: "Prouducts",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Prouducts_MenuImageId",
                table: "Products",
                newName: "IX_Products_MenuImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Prouducts_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MenuImages_MenuImageId",
                table: "Products",
                column: "MenuImageId",
                principalTable: "MenuImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_MenuImages_MenuImageId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Prouducts");

            migrationBuilder.RenameIndex(
                name: "IX_Products_MenuImageId",
                table: "Prouducts",
                newName: "IX_Prouducts_MenuImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Prouducts",
                newName: "IX_Prouducts_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prouducts",
                table: "Prouducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prouducts_Categories_CategoryId",
                table: "Prouducts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prouducts_MenuImages_MenuImageId",
                table: "Prouducts",
                column: "MenuImageId",
                principalTable: "MenuImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
