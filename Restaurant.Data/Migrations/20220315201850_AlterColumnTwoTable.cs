using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class AlterColumnTwoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingAdresses_AspNetUsers_AppUserId1",
                table: "BillingAdresses");

            migrationBuilder.DropForeignKey(
                name: "FK_FullOrders_AspNetUsers_AppUserId1",
                table: "FullOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_FullOrders_AppUserId1",
                table: "FullOrders");

            migrationBuilder.DropIndex(
                name: "IX_BillingAdresses_AppUserId1",
                table: "BillingAdresses");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "FullOrders");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "BillingAdresses");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "FullOrders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "BillingAdresses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId1",
                table: "Orders",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_FullOrders_AppUserId",
                table: "FullOrders",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingAdresses_AppUserId",
                table: "BillingAdresses",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingAdresses_AspNetUsers_AppUserId",
                table: "BillingAdresses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FullOrders_AspNetUsers_AppUserId",
                table: "FullOrders",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId1",
                table: "Orders",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingAdresses_AspNetUsers_AppUserId",
                table: "BillingAdresses");

            migrationBuilder.DropForeignKey(
                name: "FK_FullOrders_AspNetUsers_AppUserId",
                table: "FullOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_FullOrders_AppUserId",
                table: "FullOrders");

            migrationBuilder.DropIndex(
                name: "IX_BillingAdresses_AppUserId",
                table: "BillingAdresses");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "FullOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "FullOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "BillingAdresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "BillingAdresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_FullOrders_AppUserId1",
                table: "FullOrders",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BillingAdresses_AppUserId1",
                table: "BillingAdresses",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingAdresses_AspNetUsers_AppUserId1",
                table: "BillingAdresses",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FullOrders_AspNetUsers_AppUserId1",
                table: "FullOrders",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
