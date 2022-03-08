using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class AddNewColumnReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MenuImages_MenuImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Positions_PositionId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCheck",
                table: "Reservations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsClose",
                table: "Reservations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "MenuImageId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MenuImages_MenuImageId",
                table: "Products",
                column: "MenuImageId",
                principalTable: "MenuImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Positions_PositionId",
                table: "Teams",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MenuImages_MenuImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Positions_PositionId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "IsCheck",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsClose",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "Teams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "MenuImageId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_MenuImages_MenuImageId",
                table: "Products",
                column: "MenuImageId",
                principalTable: "MenuImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Positions_PositionId",
                table: "Teams",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
