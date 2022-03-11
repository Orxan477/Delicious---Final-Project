using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class SpecialTableColumnNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Positions_PositionId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Specials_MenuImages_MenuImageId",
                table: "Specials");

            migrationBuilder.DropColumn(
                name: "PropContent",
                table: "Specials");

            migrationBuilder.DropColumn(
                name: "PropContentItalic",
                table: "Specials");

            migrationBuilder.DropColumn(
                name: "PropHead",
                table: "Specials");

            migrationBuilder.AlterColumn<int>(
                name: "MenuImageId",
                table: "Specials",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InformationTabContent",
                table: "Specials",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InformationTabHead",
                table: "Specials",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InformationTabItalicContent",
                table: "Specials",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "Feedbacks",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Positions_PositionId",
                table: "Feedbacks",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specials_MenuImages_MenuImageId",
                table: "Specials",
                column: "MenuImageId",
                principalTable: "MenuImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Positions_PositionId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Specials_MenuImages_MenuImageId",
                table: "Specials");

            migrationBuilder.DropColumn(
                name: "InformationTabContent",
                table: "Specials");

            migrationBuilder.DropColumn(
                name: "InformationTabHead",
                table: "Specials");

            migrationBuilder.DropColumn(
                name: "InformationTabItalicContent",
                table: "Specials");

            migrationBuilder.AlterColumn<int>(
                name: "MenuImageId",
                table: "Specials",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "PropContent",
                table: "Specials",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PropContentItalic",
                table: "Specials",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PropHead",
                table: "Specials",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "Feedbacks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Positions_PositionId",
                table: "Feedbacks",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Specials_MenuImages_MenuImageId",
                table: "Specials",
                column: "MenuImageId",
                principalTable: "MenuImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
