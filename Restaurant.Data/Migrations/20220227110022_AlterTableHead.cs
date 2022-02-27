using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class AlterTableHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionContent",
                table: "SectionContent");

            migrationBuilder.RenameTable(
                name: "SectionContent",
                newName: "SectionContents");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "SectionHeads",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "SectionHeads",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "SectionContents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionContents",
                table: "SectionContents",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionContents",
                table: "SectionContents");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "SectionHeads");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "SectionContents");

            migrationBuilder.RenameTable(
                name: "SectionContents",
                newName: "SectionContent");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "SectionHeads",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionContent",
                table: "SectionContent",
                column: "Id");
        }
    }
}
