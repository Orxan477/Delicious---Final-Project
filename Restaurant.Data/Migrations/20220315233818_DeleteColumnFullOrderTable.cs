using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class DeleteColumnFullOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FullOrders_AspNetUsers_AppUserId",
                table: "FullOrders");

            migrationBuilder.DropIndex(
                name: "IX_FullOrders_AppUserId",
                table: "FullOrders");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "FullOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "FullOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FullOrders_AppUserId",
                table: "FullOrders",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FullOrders_AspNetUsers_AppUserId",
                table: "FullOrders",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
