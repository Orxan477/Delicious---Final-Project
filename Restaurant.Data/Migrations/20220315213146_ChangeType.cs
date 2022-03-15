using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class ChangeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId1",
                table: "Orders",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId1",
                table: "Orders",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
