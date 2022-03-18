using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class ChangeCOlumnDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FullOrders_BillingAdresses_BillingAdressId",
                table: "FullOrders");

            migrationBuilder.DropTable(
                name: "BillingAdresses");

            migrationBuilder.DropIndex(
                name: "IX_FullOrders_BillingAdressId",
                table: "FullOrders");

            migrationBuilder.DropColumn(
                name: "BillingAdressId",
                table: "FullOrders");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "FullOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAdress",
                table: "FullOrders",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "BillingAdress",
                table: "FullOrders");

            migrationBuilder.AddColumn<int>(
                name: "BillingAdressId",
                table: "FullOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BillingAdresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingAdresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingAdresses_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FullOrders_BillingAdressId",
                table: "FullOrders",
                column: "BillingAdressId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingAdresses_AppUserId",
                table: "BillingAdresses",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FullOrders_BillingAdresses_BillingAdressId",
                table: "FullOrders",
                column: "BillingAdressId",
                principalTable: "BillingAdresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
